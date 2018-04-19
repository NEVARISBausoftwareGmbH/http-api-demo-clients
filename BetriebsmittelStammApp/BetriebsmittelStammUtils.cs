using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpApiClient.Client;
using Newtonsoft.Json;

namespace BetriebsmittelStammApp
{
    public class BetriebsmittelStammKopierEventArgs
    {
        public BetriebsmittelStammKopierEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    public static class BetriebsmittelStammUtils
    {
        public static async Task<BetriebsmittelStamm> BetriebsmittelstammKopieren(
            IStammApi quellStammApi,
            IStammApi zielStammApi,
            Guid quellStammId,
            string zielStammBezeichnung,
            Action<BetriebsmittelStammKopierEventArgs> messageCallback)
        {
            var quellStamm = await quellStammApi.GetBetriebsmittelStamm(quellStammId).ConfigureAwait(false);
            var zielStämme = await zielStammApi.GetBetriebsmittelStämme();

            // Erzeuge Zielstamm, falls noch nicht vorhanden
            var zielStamm = zielStämme.SingleOrDefault(s => s.Bezeichnung == zielStammBezeichnung);
            if (zielStamm == null)
            {
                messageCallback?.Invoke(new BetriebsmittelStammKopierEventArgs($"Erzeuge Betriebsmittelstamm '{zielStammBezeichnung}', da noch nicht vorhanden."));

                zielStamm = await zielStammApi.CreateBetriebsmittelStamm(new NewBetriebsmittelStammInfo
                {
                    Art = quellStamm.Art,
                    Bezeichnung = zielStammBezeichnung
                });
            }
            else if (zielStamm.Id == quellStammId)
            {
                throw new InvalidOperationException("Quell- und Zielstamm sind identisch.");
            }

            messageCallback?.Invoke(new BetriebsmittelStammKopierEventArgs($"Abgleich der Basis-Eigenschaften von Stamm '{quellStamm.Bezeichnung}' nach Stamm '{zielStammBezeichnung}'"));

            // Anstatt den zielStamm von Hand mit den quellStamm-Properties zu befüllen,
            // klonen wir einfach den Quellstamm und setzen nur die abweichenden Properties neu.
            var clonedQuellStamm = Clone(quellStamm);
            clonedQuellStamm.Art = zielStamm.Art;
            clonedQuellStamm.Nummer = zielStamm.Nummer;
            clonedQuellStamm.Bezeichnung = zielStamm.Bezeichnung;

            // Schreibe geänderten Zielstamm zurück. Die Betriebsmittel sind hier
            // nicht enthalten. Die werden weiter unten kopiert.
            await zielStammApi.UpdateBetriebsmittelStamm(zielStamm.Id, clonedQuellStamm);

            // Hole Zielstamm neu, um die tatsächlichen IDs (z.B. für die Kostenkataloge) zu erhalten
            zielStamm = await zielStammApi.GetBetriebsmittelStamm(zielStamm.Id);

            // Jetzt sind die Betriebsmittel zu kopieren. Da diese z.T. ID-Verweise auf den zugehörigen Kosten-/Zuschlagskatalog enthalten,
            // brauchen wir ein Mapping der IDs, um diese Verweise bei der Übernahme in den neuen Zielstamm entsprechend umzubiegen.
            // dazu erzeugen wir Mappings von Kosten-/Zuschlagskatalogen (Quelle => Ziel) auf Basis von Nummer + Bezeichnung
            // (ein anderes Kriterium haben wir leider nicht).

            var kostenkatalogIds = quellStamm.Kostenkataloge.Select(qk => new
            {
                QuellId = qk.Id,
                ZielId = zielStamm.Kostenkataloge.Single(zk => zk.Nummer == qk.Nummer && zk.Bezeichnung == qk.Bezeichnung).Id
            });

            var zuschlagkatalogIds = quellStamm.Zuschlagskataloge.Select(qk => new
            {
                QuellId = qk.Id,
                ZielId = zielStamm.Zuschlagskataloge.Single(zk => zk.Nummer == qk.Nummer && zk.Bezeichnung == qk.Bezeichnung).Id
            });

            var katalogIdMap = kostenkatalogIds.Concat(zuschlagkatalogIds).ToDictionary(k => k.QuellId, k => k.ZielId);

            AbgleichInfo abgleichInfo = new AbgleichInfo
            {
                QuellStamm = quellStamm,
                ZielStamm = zielStamm,
                KatalogIdMap = katalogIdMap
            };

            // Für jede Betriebsmittelart: Gleiche Betriebsmittel ab (ohne weitere Kosten).
            foreach (var betriebsmittelArt in new[] { BetriebsmittelArt.Lohn, BetriebsmittelArt.Material, BetriebsmittelArt.Gerät, BetriebsmittelArt.SonstigeKosten, BetriebsmittelArt.Nachunternehmer, BetriebsmittelArt.Baustein })
            {
                messageCallback?.Invoke(new BetriebsmittelStammKopierEventArgs($"Abgleich der Betriebsmittel der Art '{betriebsmittelArt}'"));

                // Hole für den Quell- und Zielstamm den Betriebsmittelbaum in strukturierter Form. Dieser Aufruf liefert ein
                // Array mit einem Element, und zwar der Wurzelgruppe, die alle anderen Elemente in strukturierter Form enthält.
                var quellWurzelGruppe = (await quellStammApi.GetBetriebsmittelList(quellStamm.Id, betriebsmittelArt)).Single();
                var zielWurzelGruppe = (await zielStammApi.GetBetriebsmittelList(zielStamm.Id, betriebsmittelArt)).Single();

                // Gleiche Betriebsmittel ab (auf Basis der Nummer)
                await BetriebsmittelAbgleichen(quellStammApi, zielStammApi, abgleichInfo, quellWurzelGruppe, zielWurzelGruppe, messageCallback);
            }

            // Alle Betriebsmittel wurden übernommen. Jetzt können die weiteren Kosten übernommen werden.

            Dictionary<Guid, Guid> quellToZielBetriebsmittelIds = abgleichInfo.QuellToZielBetriebsmittel.ToDictionary(t => t.Key.Id, t => t.Value.Id);
            foreach (var quellZielBetriebsmittel in abgleichInfo.QuellToZielBetriebsmittel)
            {
                var quellBetriebsmittel = quellZielBetriebsmittel.Key;
                var zielBetriebsmittel = quellZielBetriebsmittel.Value;

                if (quellBetriebsmittel.WeitereKosten?.Count > 0)
                {
                    messageCallback?.Invoke(new BetriebsmittelStammKopierEventArgs($"Abgleich der weiteren Kosten für das Betriebsmittel {quellBetriebsmittel.GetBetriebsmittelDescription()}"));

                    zielBetriebsmittel.WeitereKosten = Clone(quellBetriebsmittel.WeitereKosten);

                    void TransformBetriebsmittelLinks(IEnumerable<KalkulationsZeile> kalkZeilen)
                    {
                        foreach (var kalkZeile in kalkZeilen)
                        {
                            if (kalkZeile.BetriebsmittelDetails != null)
                            {
                                kalkZeile.BetriebsmittelDetails.BetriebsmittelId = quellToZielBetriebsmittelIds[kalkZeile.BetriebsmittelDetails.BetriebsmittelId];
                            }
                            else if (kalkZeile.UnterpositionDetails != null)
                            {
                                TransformBetriebsmittelLinks(kalkZeile.UnterpositionDetails.Zeilen);
                            }
                        }
                    }

                    TransformBetriebsmittelLinks(zielBetriebsmittel.WeitereKosten);

                    zielBetriebsmittel.Kosten = null;
                    zielBetriebsmittel.Zuschläge = null;
                    await zielStammApi.UpdateBetriebsmittel(zielBetriebsmittel.Id, zielBetriebsmittel);
                }
            }

            return zielStamm;
        }

        static string GetBetriebsmittelDescription(this Betriebsmittel betriebsmittel)
        {
            return string.IsNullOrEmpty(betriebsmittel.Bezeichnung) ? betriebsmittel.NummerKomplett : $"{betriebsmittel.NummerKomplett} ('{betriebsmittel.Bezeichnung}')";
        }

        class AbgleichInfo
        {
            public BetriebsmittelStamm QuellStamm { get; set; }
            public BetriebsmittelStamm ZielStamm { get; set; }
            public Dictionary<Guid, Guid> KatalogIdMap { get; set; }

            public Dictionary<Betriebsmittel, Betriebsmittel> QuellToZielBetriebsmittel { get; } = new Dictionary<Betriebsmittel, Betriebsmittel>();
        }

        static async Task BetriebsmittelAbgleichen(
            IStammApi quellStammApi,
            IStammApi zielStammApi,
            AbgleichInfo abgleichInfo,
            Betriebsmittel quellGruppe,
            Betriebsmittel zielGruppe,
            Action<BetriebsmittelStammKopierEventArgs> messageCallback)
        {
            var quellChildren = quellGruppe.GruppeDetails.BetriebsmittelList.Where(b => b.Nummer != null).ToList();

            var zielChildrenByNumber = zielGruppe.GruppeDetails.BetriebsmittelList.Where(b => b.Nummer != null).ToDictionary(b => b.Nummer);

            foreach (var quellChildItem in quellChildren)
            {
                // Lade Quell-Betriebsmittel inkl. Details
                var quellChild = await quellStammApi.GetBetriebsmittel(quellChildItem.Id, quellChildItem.Art);

                messageCallback?.Invoke(new BetriebsmittelStammKopierEventArgs($"Abgleich des Betriebsmittels {quellChild.GetBetriebsmittelDescription()}"));

                zielChildrenByNumber.TryGetValue(quellChild.Nummer, out var zielChild);

                // Falls das Betriebsmittel noch keine Entsprechung im Ziel hat => neu erzeugen (gilt auch für Gruppen)
                if (zielChild == null)
                {
                    zielChild = await zielStammApi.CreateBetriebsmittel(abgleichInfo.ZielStamm.Id, new NewBetriebsmittelInfo
                    {
                        Art = quellChild.Art,
                        Bezeichnung = quellChild.Bezeichnung,
                        Nummer = quellChild.Nummer,
                        ParentGruppeId = zielGruppe.Id
                    });

                    if (zielChild.GruppeDetails != null)
                    {
                        zielChild.GruppeDetails.BetriebsmittelList = new List<Betriebsmittel>();
                    }
                }

                abgleichInfo.QuellToZielBetriebsmittel.Add(quellChild, zielChild);

                zielChild.Bezeichnung = quellChild.Bezeichnung;
                zielChild.Leistungsfähig = quellChild.Leistungsfähig;
                zielChild.Einheit = quellChild.Einheit;
                zielChild.Kostenart = quellChild.Kostenart;

                zielChild.Kosten = quellChild.Kosten?.Select(k => new BetriebsmittelKosten
                {
                    KostenebeneId = abgleichInfo.KatalogIdMap[k.KostenebeneId],
                    LohnDetails = Clone(k.LohnDetails),
                    GerätDetails = Clone(k.GerätDetails),
                    MaterialDetails = Clone(k.MaterialDetails),
                    SonstigeKostenDetails = Clone(k.SonstigeKostenDetails),
                    NachunternehmerDetails = Clone(k.NachunternehmerDetails)
                }).ToList();

                zielChild.Zuschläge = quellChild.Zuschläge?.Select(bz => new BetriebsmittelZuschlag
                {
                    ArtIndex = bz.ArtIndex,
                    ZuschlagsgruppenNummer = bz.ZuschlagsgruppenNummer,
                    ZuschlagsebeneId = abgleichInfo.KatalogIdMap[bz.ZuschlagsebeneId],
                }).ToList();

                zielChild.Details = Clone(quellChild.Details);

                zielChild.LohnDetails = Clone(quellChild.LohnDetails);
                zielChild.MaterialDetails = Clone(quellChild.MaterialDetails);
                zielChild.GerätDetails = Clone(quellChild.GerätDetails);
                zielChild.SonstigeKostenDetails = Clone(quellChild.SonstigeKostenDetails);
                zielChild.NachunternehmerDetails = Clone(quellChild.NachunternehmerDetails);
                zielChild.BausteinDetails = Clone(quellChild.BausteinDetails);

                zielChild.WeitereKosten = null;

                await zielStammApi.UpdateBetriebsmittel(zielChild.Id, zielChild);

                // Falls das zielChild eine Gruppe ist => rekursiv die Kinder abgleichen
                if (zielChild.GruppeDetails != null)
                {
                    await BetriebsmittelAbgleichen(quellStammApi, zielStammApi, abgleichInfo, quellChildItem, zielChild, messageCallback);
                }
            }
        }

        static T Clone<T>(T source) where T : class
        {
            if (source == null) return null;

            string serialized = JsonConvert.SerializeObject(source, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}

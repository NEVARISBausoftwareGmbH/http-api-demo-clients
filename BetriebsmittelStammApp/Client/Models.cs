using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HttpApiClient.Client
{
    public class BaseObject
    {
#pragma warning disable CS0169
        [JsonExtensionData]
        IDictionary<string, JToken> _additionalData;
#pragma warning restore CS0169
    }

    public class Speicherort : BaseObject
    {
        public Guid Id { get; set; }
        public string Bezeichnung { get; set; }
        public OrdnerInfo OrdnerInfo { get; set; }
        public DatenbankInfo DatenbankInfo { get; set; }
        public List<ProjektInfo> ProjektInfos { get; set; }
    }

    public class ProjektInfo : BaseObject
    {
        public string Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    public class OrdnerInfo : BaseObject
    {
        public string Pfad { get; set; }
        public string Arbeitsgruppenserver { get; set; }
    }

    public class DatenbankInfo : BaseObject
    {
        public string Server { get; set; }
        public string Datenbank { get; set; }
        public string Benutzername { get; set; }
        public string Passwort { get; set; }
        public bool IntegratedSecurity { get; set; }
    }

    public class NewProjektInfo : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    public enum Norm
    {
        Oenorm,
        Gaeb
    }

    public class Leistungsverzeichnis : BaseObject
    {
        public Guid Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public Norm? Norm { get; set; }
        public string Waehrung { get; set; }
        public LVKnoten Wurzelknoten { get; set; }
    }

    public class LVItemBase : BaseObject
    {
        public Guid Id { get; set; }
        public string Typ { get; set; }
        public string Nummer { get; set; }
        public string NummerKomplett { get; set; }
        public string Kurztext { get; set; }
        public string Teilleistungsnummer { get; set; }
        public decimal? Betrag { get; set; }
    }

    public class LVKnoten : LVItemBase
    {
        public List<LVKnoten> Knoten { get; set; }
        public List<LVPosition> Positionen { get; set; }
    }

    public class LVPosition : LVItemBase
    {
        public string Einheit { get; set; }
        public decimal? Menge { get; set; }
    }

    public class Projekt : BaseObject
    {
        public string Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public DateTime? Baubeginn { get; set; }
        public DateTime? Bauende { get; set; }
        public string Ampel { get; set; }
        public string Art { get; set; }
        public string Auschreibungsart { get; set; }
        public string Status { get; set; }
        public string Sparte { get; set; }
        public string Typ { get; set; }
        public List<LeistungsverzeichnisItem> LeistungsverzeichnisItems { get; set; }
    }

    public class LeistungsverzeichnisItem : BaseObject
    {
        public Guid Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    public enum GeräteArt
    {
        Vorhaltegerät,
        Leistungsgerät,
        Investitionsgerät,
        Listenpreisgerät
    }

    public class Gerätefaktor : BaseObject
    {
        public string Nummer { get; set; }
        public GeräteArt? Art { get; set; }
        public string Bezeichnung { get; set; }
        public decimal? AbminderungsfaktorAV { get; set; }
        public decimal? AbminderungsfaktorRepLohn { get; set; }
        public decimal? AbminderungsfaktorRepMaterial { get; set; }
        public decimal? StundenProMonat { get; set; }
    }

    public class GlobaleVariable : BaseObject
    {
        public string Variable { get; set; }
        public bool? IstKalkulationsVariable { get; set; }
        public string Ansatz { get; set; }
    }

    public class Warengruppe : BaseObject
    {
        public int Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    public class BetriebsmittelStammBezeichnungen : BaseObject
    {
        public string LohnKostenanteil1 { get; set; }
        public string LohnKostenanteil2 { get; set; }

        public string SonstigeKostenKostenanteil1 { get; set; }
        public string SonstigeKostenKostenanteil2 { get; set; }
        public string SonstigeKostenKostenanteil3 { get; set; }
        public string SonstigeKostenKostenanteil4 { get; set; }
        public string SonstigeKostenKostenanteil5 { get; set; }
        public string SonstigeKostenKostenanteil6 { get; set; }

        public string NachunternehmerKostenanteil1 { get; set; }
        public string NachunternehmerKostenanteil2 { get; set; }
        public string NachunternehmerKostenanteil3 { get; set; }
        public string NachunternehmerKostenanteil4 { get; set; }
        public string NachunternehmerKostenanteil5 { get; set; }
        public string NachunternehmerKostenanteil6 { get; set; }
    }

    public class BetriebsmittelStamm : BaseObject
    {
        public Guid Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public BetriebsmittelStammArt? Art { get; set; }

        public int? RechengenauigkeitMengen { get; set; } // = NachkommastellenAnsatz
        public int? RechengenauigkeitBeträge { get; set; } // = NachkommastellenKostenPreise
        public int? DarstellungsgenauigkeitMengen { get; set; } // = NachkommastellenAnsatzUI
        public int? DarstellungsgenauigkeitBeträge { get; set; } // = NachkommastellenKostenPreiseUI

        public BetriebsmittelStammBezeichnungen Bezeichnungen { get; set; }

        public List<Kostenkatalog> Kostenkataloge { get; set; }
        public List<Zuschlagskatalog> Zuschlagskataloge { get; set; }

        public List<Zuschlagsgruppe> Zuschlagsgruppen { get; set; }
        public List<Zuschlagsart> Zuschlagsarten { get; set; }

        public List<Gerätefaktor> Gerätefaktoren { get; set; }
        public List<GlobaleVariable> GlobaleVariablen { get; set; }
        public List<Warengruppe> Warengruppen { get; set; }

        public List<DbBetriebsmittelGruppe> DbBetriebsmittelGruppen { get; set; }

        /// <summary>
        /// Die Einträge im Grid "Zuschlagsberechnung" (nur GAEB-Stämme).
        /// </summary>
        public List<ZuschlagsartGruppe> ZuschlagsartGruppen { get; set; }
    }

    public enum BetriebsmittelStammArt
    {
        FreieForm,
        Aut,
        Ger
    }

    public class NewBetriebsmittelStammInfo : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public BetriebsmittelStammArt? Art { get; set; }
    }

    public class NewKostenkatalogInfo : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    public class NewZuschlagskatalogInfo : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    public class NewZuschlagsartInfo : BaseObject
    {
        public int Index { get; set; }
        public string Bezeichnung { get; set; }
        public ZuschlagsTyp? Zuschlagstyp { get; set; }
    }

    public class DbBetriebsmittelGruppe
    {
        public string Bezeichnung { get; set; }
        public BetriebsmittelArt? Art { get; set; }
    }

    public class ZuschlagsartGruppe : BaseObject
    {
        public ZuschlagsTyp? Art { get; set; }
        public ZuschlagsBasis? Berechnung { get; set; }
        public bool HatZwischensumme { get; set; }
    }

    public enum ZuschlagsBasis
    {
        AufHundert,
        ImHundert,
    }

    public class Zuschlagsgruppe : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public int? Stufe { get; set; }
    }

    public class NewZuschlagsgruppeInfo : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public int? Stufe { get; set; }
    }

    public class ZuschlagsgruppenWert : BaseObject
    {
        public string ZuschlagsgruppenNummer { get; set; }
        public decimal? Wert { get; set; }
    }

    public class Zuschlagsart : BaseObject
    {
        public int Index { get; set; }
        public string Bezeichnung { get; set; }
        public ZuschlagsTyp? Typ { get; set; }
    }

    public enum ZuschlagsTyp
    {
        Agk, Bgk, Gewinn, Wagnis
    }

    public class Kostenkatalog : BaseObject
    {
        public Guid Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public bool IstStandard { get; set; }

        public Guid? ParentKostenkatalogId { get; set; }
    }

    public class Zuschlagskatalog : BaseObject
    {
        public Guid Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public bool IstStandard { get; set; }

        public List<ZuschlagsgruppenWert> ZuschlagsgruppenWerte { get; set; }
    }

    public enum BetriebsmittelArt
    {
        Lohn,
        Material,
        Gerät,
        SonstigeKosten,
        Nachunternehmer,
        Baustein,

        // Spezielle Enums für Gruppen (keine Entsprechung in Entities.BetriebsmittelArt)
        LohnGruppe,
        MaterialGruppe,
        GerätGruppe,
        SonstigeKostenGruppe,
        NachunternehmerGruppe,
        BausteinGruppe
    }

    public class NewBetriebsmittelInfo : BaseObject
    {
        public Guid? ParentGruppeId { get; set; }

        public BetriebsmittelArt Art { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
    }

    public class Betriebsmittel : BaseObject
    {
        public BetriebsmittelArt Art { get; set; }
        public Guid Id { get; set; }
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }
        public string NummerKomplett { get; set; }
        public bool? Leistungsfähig { get; set; }
        public string Einheit { get; set; }
        public string Kostenart { get; set; }

        public List<BetriebsmittelKosten> Kosten { get; set; }

        public List<KalkulationsZeile> WeitereKosten { get; set; }

        public List<BetriebsmittelZuschlag> Zuschläge { get; set; }

        public BetriebsmittelDetails Details { get; set; }

        public BetriebsmittelGruppeDetails GruppeDetails { get; set; }
        public BetriebsmittelLohnDetails LohnDetails { get; set; }
        public BetriebsmittelMaterialDetails MaterialDetails { get; set; }
        public BetriebsmittelGerätDetails GerätDetails { get; set; }
        public BetriebsmittelSonstigeKostenDetails SonstigeKostenDetails { get; set; }
        public BetriebsmittelNachunternehmerDetails NachunternehmerDetails { get; set; }
        public BetriebsmittelBausteinDetails BausteinDetails { get; set; }
    }

    public class BetriebsmittelDetails : BaseObject
    {
        public string Stichwörter { get; set; }
        public string Markierungskennzeichen { get; set; }

        public string StandardAnsatz { get; set; }

        public string DbBetriebsmittelGruppeBezeichnung; // = DBBetriebsmittelgruppe
    }

    public class BetriebsmittelZuschlag : BaseObject
    {
        public string ZuschlagsgruppenNummer { get; set; }
        public string ZuschlagsgruppenBezeichnung { get; set; }
        public int ArtIndex { get; set; }
        public string ArtBezeichnung { get; set; }
        public Guid ZuschlagsebeneId { get; set; }
        public string ZuschlagsebeneTyp { get; set; }
        public decimal? Wert { get; set; }
    }

    public class BetriebsmittelGruppeDetails : BaseObject
    {
        /// <summary>
        /// Die in dieser Gruppe enthaltenen Child-Betriebsmittel.
        /// </summary>
        public List<Betriebsmittel> BetriebsmittelList { get; set; }
    }

    public class BetriebsmittelLohnDetails : BaseObject
    {
        public bool? IstProduktiv { get; set; }
        public decimal? EinheitenFaktor { get; set; } // = Umrechnung auf Stunden(1/x)
        public string BasNummer { get; set; } // Nummer des Bauarbeitsschlüssels
    }

    public class BetriebsmittelMaterialDetails : BaseObject
    {
        public decimal? Ladezeit { get; set; }
    }

    public class BetriebsmittelGerätDetails : BaseObject
    {
        public BglArt? BasisBgl { get; set; } // = BGLArt

        public string GerätefaktorNummer { get; set; }

        public string KostenartAV { get; set; }
        public string KostenartRepLohn { get; set; }
        public string KostenartRepMaterial { get; set; }

        public BetriebsmittelGerätDetailsSonstiges Sonstiges { get; set; }
    }

    public enum Antriebsart
    {
        Elektro,
        Diesel,
        Gas,
        Benzin
    }

    public class BetriebsmittelGerätDetailsSonstiges : BaseObject
    {
        public string BglNummer { get; set; } // = BGLNummer
        public string EdvKurztext { get; set; } // = BGLBezeichnung

        // Sonstiges
        public decimal? Vorhaltemenge { get; set; }
        public decimal? Vorhaltedauer { get; set; }
        public Antriebsart? Antriebsart1 { get; set; } // = AntriebsArt
        public Antriebsart? Antriebsart2 { get; set; }
        public decimal? Gewicht { get; set; }
        public decimal? TransportVolumen { get; set; }
        public decimal? Leistung1 { get; set; } // = GeraeteLeistung
        public decimal? Leistung2 { get; set; } // = GeraeteLeistung2

        // Bahnspezifische Angaben
        public string Fabrikat { get; set; }
        public int? Mindestradius { get; set; }
        public int? BeherrschbareSteigung { get; set; }
        public string Schallemissionspegel { get; set; }
        public int? Arbeitsgeschwindigkeit { get; set; }
        public int? GegenseitigeUeberhoehung { get; set; }
    }

    public class BetriebsmittelSonstigeKostenDetails : BaseObject
    {
        public string Kostenart1 { get; set; }
        public string Kostenart2 { get; set; }
        public string Kostenart3 { get; set; }
        public string Kostenart4 { get; set; }
        public string Kostenart5 { get; set; }
        public string Kostenart6 { get; set; }
    }

    public class BetriebsmittelNachunternehmerDetails : BaseObject
    {
        public string Kostenart1 { get; set; }
        public string Kostenart2 { get; set; }
        public string Kostenart3 { get; set; }
        public string Kostenart4 { get; set; }
        public string Kostenart5 { get; set; }
        public string Kostenart6 { get; set; }
    }

    public class BetriebsmittelBausteinDetails : BaseObject
    {
    }

    public class BetriebsmittelKosten : BaseObject
    {
        public Guid KostenebeneId { get; set; }
        public string KostenebeneTyp { get; set; }

        public BetriebsmittelKostenLohnDetails LohnDetails { get; set; }
        public BetriebsmittelKostenMaterialDetails MaterialDetails { get; set; }
        public BetriebsmittelKostenGerätDetails GerätDetails { get; set; }
        public BetriebsmittelKostenSonstigeKostenDetails SonstigeKostenDetails { get; set; }
        public BetriebsmittelKostenNachunternehmerDetails NachunternehmerDetails { get; set; }
    }

    public class BetriebsmittelKostenLohnDetails : BaseObject
    {
        public Money Kostenanteil1 { get; set; }
        public Money Kostenanteil2 { get; set; }

        public int? WarengruppeNummer { get; set; }
    }

    public class BetriebsmittelKostenMaterialDetails : BaseObject
    {
        public Money Listenpreis { get; set; }
        public decimal? Rabatt { get; set; }
        public decimal? Verlust { get; set; }
        public Money Manipulation { get; set; }
        public Money Transportkosten { get; set; }

        public int? WarengruppeNummer { get; set; }
    }

    public class BetriebsmittelKostenGerätDetails : BaseObject
    {
        public Money Neuwert { get; set; } // = Mittlerer Neuwert
        public Money Kostenanteil1 { get; set; }
        public Money Kostenanteil2 { get; set; }
        public decimal? AbschreibungVerzinsung { get; set; } // = A + V
        public decimal? Reparaturkosten { get; set; }
    }

    public class BetriebsmittelKostenSonstigeKostenDetails : BaseObject
    {
        public Money Kostenanteil1 { get; set; }
        public Money Kostenanteil2 { get; set; }
        public Money Kostenanteil3 { get; set; }
        public Money Kostenanteil4 { get; set; }
        public Money Kostenanteil5 { get; set; }
        public Money Kostenanteil6 { get; set; }

        public int? Warengruppe0Nummer { get; set; }
        public int? Warengruppe1Nummer { get; set; }
        public int? Warengruppe2Nummer { get; set; }
        public int? Warengruppe3Nummer { get; set; }
        public int? Warengruppe4Nummer { get; set; }
        public int? Warengruppe5Nummer { get; set; }
    }

    public class BetriebsmittelKostenNachunternehmerDetails : BaseObject
    {
        public Money Kostenanteil1 { get; set; }
        public Money Kostenanteil2 { get; set; }
        public Money Kostenanteil3 { get; set; }
        public Money Kostenanteil4 { get; set; }
        public Money Kostenanteil5 { get; set; }
        public Money Kostenanteil6 { get; set; }

        public int? Warengruppe0Nummer { get; set; }
        public int? Warengruppe1Nummer { get; set; }
        public int? Warengruppe2Nummer { get; set; }
        public int? Warengruppe3Nummer { get; set; }
        public int? Warengruppe4Nummer { get; set; }
        public int? Warengruppe5Nummer { get; set; }
    }

    /// <summary>
    /// Eine Zeile in den weiteren Kosten.
    /// </summary>
    public class KalkulationsZeile : BaseObject
    {
        public string Nummer { get; set; }
        public string Bezeichnung { get; set; }

        public KalkulationsZeileBetriebsmittelDetails BetriebsmittelDetails { get; set; }
        public KalkulationsZeileVariablenDetails VariablenDetails { get; set; }
        public KalkulationsZeileKommentarDetails KommentarDetails { get; set; }
        public KalkulationsZeileUnterpositionDetails UnterpositionDetails { get; set; }
    }

    public class KalkulationsZeileBetriebsmittelDetails : BaseObject
    {
        public Guid BetriebsmittelId { get; set; }
        // TODO Betriebsmittelart
        public string Ansatz { get; set; }
        public string BasNummer { get; set; }
    }

    public class KalkulationsZeileVariablenDetails : BaseObject
    {
        public string Variable { get; set; }
        public string Ansatz { get; set; }
    }

    public class KalkulationsZeileKommentarDetails : BaseObject
    {
        public string Kommentar { get; set; }
    }

    public class KalkulationsZeileUnterpositionDetails : BaseObject
    {
        public string Ansatz { get; set; }
        public string BasNummer { get; set; }
        public List<KalkulationsZeile> Zeilen { get; set; }
    }

    public enum BglArt
    {
        Keine,
        Oebgl,
        Dbgl
    }

    public class SimpleMoney
    {
        public SimpleMoney(string currency, decimal? value)
        {
            Currency = currency;
            Value = value;
        }

        [JsonProperty("cur")]
        public string Currency { get; private set; }

        [JsonProperty("val")]
        public decimal? Value { get; private set; }
    }

    public class Money : Collection<SimpleMoney>
    {
        public Money()
        {
        }

        public Money(IEnumerable<(string Code, decimal? Value)> beträge)
        {
            foreach (var betrag in beträge)
            {
                Add(new SimpleMoney(betrag.Code, betrag.Value));
            }
        }

        public void Add(string currency, decimal value)
        {
            Add(new SimpleMoney(currency, value));
        }

        public decimal? FirstValue
        {
            get => this.FirstOrDefault()?.Value;
            set => this[0] = new SimpleMoney(this[0].Currency, value);
        }
    }
}

using Nevaris.Build.ClientApi;
using Newtonsoft.Json;

namespace LvKopierenApp;

/// <summary>
/// Ein Testprogramm, das den Zugriff auf Adressen über die NEVARIS Build API demonstriert.
/// </summary>
static class Program
{
    static async Task Main(string[] args)
    {
        Settings settings;

        try
        {
            Console.WriteLine("Lese Einstellungen aus Settings.json ...");
            string settingsJson = await File.ReadAllTextAsync("Settings.json");
            settings = JsonConvert.DeserializeObject<Settings>(settingsJson)
                       ?? throw new InvalidOperationException("Fehler beim Auslesen von Settings.json");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Auf Settings.json konnte nicht zugegriffen werden ({ex.GetType().Name}).");
            return;
        }

        try
        {
            string baseUrl = settings.BaseUrl ?? throw new InvalidOperationException("BaseUrl erforderlich");

            Console.WriteLine($"Stelle Verbindung zu {baseUrl} her ...");

            using var client = new NevarisBuildClient(baseUrl, new NevarisBuildClientOptions { Timeout = TimeSpan.FromSeconds(10000) });

            var versionCheckResult = await client.CheckVersion();
            if (!versionCheckResult.AreVersionsCompatible)
            {
                throw new InvalidOperationException($"Versionskonflikt: API: {versionCheckResult.ApiVersion}, client: {versionCheckResult.ClientVersion}");
            }

            var speicherorte = await client.StammApi.GetSpeicherorte();
            var speicherort = speicherorte.FirstOrDefault(s => s.Bezeichnung == settings.SpeicherortBezeichnung) 
                ?? throw new InvalidOperationException($"{settings.SpeicherortBezeichnung}: Speicherort nicht gefunden");

            speicherort = await client.StammApi.GetSpeicherort(speicherort.Id);
            var projektInfo = speicherort.ProjektInfos.FirstOrDefault(s => s.Bezeichnung == settings.ProjektBezeichnung)
                ?? throw new InvalidOperationException($"{settings.ProjektBezeichnung}: Projekt nicht gefunden");

            var projekt = await client.ProjektApi.GetProjekt(projektInfo.Id);
     
            var quellLv = projekt.Leistungsverzeichnisse.FirstOrDefault(l => l.Bezeichnung == settings.LvBezeichnungQuelle)
                ?? throw new InvalidOperationException($"{settings.LvBezeichnungQuelle}: LV nicht gefunden");

            // Quell-LV vollständig (d.h. mit allen Knoten und Positionen laden)
            quellLv = await client.ProjektApi.GetLeistungsverzeichnis(projekt.Id, quellLv.Id, mitKalkulationen: false);

            var zielLv = projekt.Leistungsverzeichnisse.FirstOrDefault(l => l.Bezeichnung == settings.LvBezeichnungZiel);
            if (zielLv != null)
            {
                await client.ProjektApi.DeleteLeistungsverzeichnis(projekt.Id, zielLv.Id);
            }

            zielLv = await client.ProjektApi.CreateLeistungsverzeichnis(projekt.Id, new NewLvInfo
            {
                Art = quellLv.Art,
                Nummer = quellLv.Nummer,
                Bezeichnung = settings.LvBezeichnungZiel,
                LvDetails = quellLv.LvDetails,
                OenormLvDetails = quellLv.OenormLvDetails,
                GaebLvDetails = quellLv.GaebLvDetails,
                NormExakt = quellLv.NormExakt,
                Status = quellLv.Status,
                BildDetails = quellLv.BildDetails
            });
            
            foreach (var quellPosition in quellLv.RootPositionen)
            {
                await KopierePosition(
                    client.ProjektApi, 
                    projektId: projekt.Id,
                    quellPosition, 
                    zielLvId: zielLv.Id,
                    zielParentKnoten: null);
            }

            foreach (var quellKnoten in quellLv.RootKnotenListe)
            {
                await KopiereKnoten(
                    client.ProjektApi,
                    projektId: projekt.Id,
                    quellKnoten: quellKnoten,
                    zielLvId: zielLv.Id,
                    zielParentKnoten: null);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler: {ex.Message}");
        }
    }

    static async Task KopiereKnoten(
        IProjektApi api, 
        string projektId,
        LvKnoten quellKnoten,
        Guid zielLvId,
        LvKnoten? zielParentKnoten)
    {
        var zielKnoten = await api.CreateLvKnoten(projektId, zielLvId, new NewLvKnotenInfo
        {
            ItemTyp = quellKnoten.ItemTyp,
            ParentKnotenId = zielParentKnoten?.Id,
            Herkunftskennzeichen = quellKnoten.Herkunftskennzeichen,
            IstFixpreis = quellKnoten.IstFixpreis,
            IstIntern = quellKnoten.IstIntern,
            Stichwort = quellKnoten.Stichwort, // nur für ÖNorm-LVs relevant
            FormatierteTexte = quellKnoten.FormatierteTexte,
            Markierungskennzeichen = quellKnoten.Markierungskennzeichen,
            LbInfo = quellKnoten.LbInfo,
            Nummer = quellKnoten.Nummer,
            Teilleistungsnummer = quellKnoten.Teilleistungsnummer,
            Zuordnungskennzeichen = quellKnoten.Zuordnungskennzeichen,
            Variante = quellKnoten.Variante,
            NachlassInfo = quellKnoten.NachlassInfo,
            Schreibgeschützt = quellKnoten.Schreibgeschützt,
            Entfällt = quellKnoten.Entfällt
        });

        foreach (var quellPosition in quellKnoten.Positionen)
        {
            await KopierePosition(api, projektId, quellPosition, zielLvId, zielKnoten);
        }

        foreach (var childQuellKnoten in quellKnoten.Knoten)
        {
            await KopiereKnoten(
                api,
                projektId: projektId, 
                quellKnoten: childQuellKnoten, 
                zielLvId: zielLvId,
                zielParentKnoten: zielKnoten);
        }
    }
    
    static async Task KopierePosition(
        IProjektApi api,
        string projektId,
        LvPosition quellPosition,
        Guid zielLvId,
        LvKnoten? zielParentKnoten)
    {
        await api.CreateLvPosition(
            projektId, zielLvId, new NewLvPositionInfo
            {
                ItemTyp = quellPosition.ItemTyp,
                ParentKnotenId = zielParentKnoten?.Id,
                Herkunftskennzeichen = quellPosition.Herkunftskennzeichen,
                IstFixpreis = quellPosition.IstFixpreis,
                IstIntern = quellPosition.IstIntern,
                Schreibgeschützt = quellPosition.Schreibgeschützt,
                Stichwort = quellPosition.Stichwort, // nur für ÖNorm-LVs relevant
                FormatierteTexte = quellPosition.FormatierteTexte,
                Markierungskennzeichen = quellPosition.Markierungskennzeichen,
                LbInfo = quellPosition.LbInfo,
                Nummer = quellPosition.Nummer,
                Teilleistungsnummer = quellPosition.Teilleistungsnummer,
                LvMenge = quellPosition.LvMenge,
                Positionsart = quellPosition.Positionsart,
                Einheit = quellPosition.Einheit,
                IstWesentlichePosition = quellPosition.IstWesentlichePosition,
                Preisanteile = quellPosition.Preisanteile,
                GliederungsKnotenList = quellPosition.GliederungsKnotenList,
                Zuordnungskennzeichen = quellPosition.Zuordnungskennzeichen,
                Variante = quellPosition.Variante,
                NachlassInfo = quellPosition.NachlassInfo,
                EinheitSchreibgeschützt = quellPosition.EinheitSchreibgeschützt,
                EinheitspreisSchreibgeschützt = quellPosition.EinheitspreisSchreibgeschützt,
                LvMengeSchreibgeschützt = quellPosition.LvMengeSchreibgeschützt,
                Mehrfachverwendung = quellPosition.Mehrfachverwendung,
                Stichwortluecke = quellPosition.Stichwortluecke,
                TexteSchreibgeschützt = quellPosition.TexteSchreibgeschützt,
                BedarfspositionArt = quellPosition.BedarfspositionArt,
                IstBeauftragt = quellPosition.IstBeauftragt,
                IstSchwerpunktposition = quellPosition.IstSchwerpunktposition,
                IstStundenlohnarbeiten = quellPosition.IstStundenlohnarbeiten,
                WirdBezuschlagt = quellPosition.WirdBezuschlagt,
                IstFreieBietermenge = quellPosition.IstFreieBietermenge,
                IstNichtAngeboten = quellPosition.IstNichtAngeboten,
                Entfällt = quellPosition.Entfällt,
                Zuschlagsprozentsatz = quellPosition.Zuschlagsprozentsatz
            });
    }
}

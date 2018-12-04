using Nevaris.Build.ClientApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrechnungConsoleApp
{
    public class Settings
    {
        public string BaseUrl { get; set; }

        public string SpeicherortBezeichnung { get; set; }
        public string ProjektNummer { get; set; }
        public string ProjektBezeichnung { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Settings settings = null;

            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            try
            {
                Console.WriteLine("Lese Einstellungen aus Settings.json ...");
                string settingsJson = File.ReadAllText("Settings.json");
                settings = JsonConvert.DeserializeObject<Settings>(settingsJson);
            }
            catch (Exception)
            {
                Console.WriteLine("Auf Settings.json konnte nicht zugegriffen werden.");
                return;
            }

            try
            {
                string baseUrl = settings.BaseUrl ?? "http://localhost:8500";

                Console.WriteLine($"Stelle Verbindung zu {baseUrl} her ...");

                var client = new NevarisBuildClient(baseUrl);

                var speicherorte = await client.StammApi.GetSpeicherorte();

                var speicherort = speicherorte.FirstOrDefault(s => s.Bezeichnung == settings.SpeicherortBezeichnung)
                    ?? throw new InvalidOperationException($"Speicherort '{settings.SpeicherortBezeichnung}' nicht gefunden");

                speicherort = await client.StammApi.GetSpeicherort(speicherort.Id);

                var projektInfo = speicherort.ProjektInfos.FirstOrDefault(p => p.Nummer == settings.ProjektNummer && p.Bezeichnung == settings.ProjektBezeichnung)
                    ?? throw new InvalidOperationException($"Projekt '{settings.ProjektNummer} – {settings.ProjektBezeichnung}' nicht gefunden");

                var projekt = await client.ProjektApi.GetProjekt(projektInfo.Id);

                foreach (var lv in projekt.Leistungsverzeichnisse)
                {
                    if (lv.Art == LvArt.AuftragAusfuehrend)
                    {
                        await VerarbeiteAuftrag(client.ProjektApi, projekt, lv);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }
        }

        static async Task VerarbeiteAuftrag(IProjektApi api, Projekt projekt, Leistungsverzeichnis lv)
        {
            var idToLeistungszeitraum = projekt.Leistungszeiträume.ToDictionary(lz => lz.Id);
            var idToRechungen = (await api.GetRechnungen(projekt.Id, lv.Id)).ToDictionary(r => r.Id);
            var idToAufmaßblätter = (await api.GetAufmaßblätter(projekt.Id, lv.Id)).ToDictionary(r => r.Id);

            var merkmale = await api.GetAbrechnungsMerkmale(projekt.Id, lv.Id);
            var positionsblöcke = await api.GetPositionsblöcke(projekt.Id, lv.Id);

            Console.WriteLine();

            foreach (var p in positionsblöcke)
            {
                StringBuilder sp = new StringBuilder();
                if (p.AufmaßblattId != null) sp.Append($"; Blatt = {idToAufmaßblätter[p.AufmaßblattId.Value].Bezeichnung}");
                if (p.LeistungszeitraumId != null) sp.Append($"; LZ = {idToLeistungszeitraum[p.LeistungszeitraumId.Value].Bezeichnung}");
                if (p.RechnungId != null) sp.Append($"; Rechnung = {idToRechungen[p.RechnungId.Value].Bezeichnung}");

                Console.WriteLine($"Positionsblock {p.Nummer}: Menge = {p.Menge}; Einheit = {p.Einheit}{sp}");

                foreach (var z in p.Aufmaßzeilen)
                {
                    StringBuilder sb = new StringBuilder();
                    if (z.Menge != null) sb.Append($"; Menge = {z.Menge}");
                    if (z.Variable != null) sb.Append($"; Variable = {z.Variable}");

                    if (z.Art == AufmaßzeilenArt.Kommentar)
                    {
                        Console.WriteLine($"    Kommentar: {z.Inhalt}");
                    }
                    else if (z.Art == AufmaßzeilenArt.Ansatz)
                    {
                        Console.WriteLine($"    Ansatz: {z.Inhalt}{sb}");
                    }
                    else if (z.Art == AufmaßzeilenArt.Formel && z.Formel != null)
                    {
                        Console.WriteLine($"    Formel: {FormelToString(z.Formel)}{sb}");
                    }
                }
            }
        }

        static string FormelToString(Formel formel)
        {
            return formel.Id + "[" + string.Join(", ", formel.Params.Select(p => $"{p.Name}={p.Value?.ToString() ?? p.Variable}")) + "]";
        }
    }
}

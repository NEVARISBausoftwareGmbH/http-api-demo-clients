using Nevaris.Build.ClientApi;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdressConsoleApp
{
    public class Settings
    {
        public string BaseUrl { get; set; }

        public string AdressName { get; set; }
    }

    /// <summary>
    /// Ein Testprogramm, das den Zugriff auf Adressen über die NEVARIS Build API demonstriert.
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            Settings settings = null;

            try
            {
                Console.WriteLine("Lese Einstellungen aus Settings.json ...");
                string settingsJson = File.ReadAllText("Settings.json");
                settings = JsonConvert.DeserializeObject<Settings>(settingsJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Auf Settings.json konnte nicht zugegriffen werden ({ex.GetType().Name}). Es werden Default-Werte angenommen.");
            }

            try
            {
                string baseUrl = settings?.BaseUrl ?? "http://localhost:8500";
                string nameTestAdresse = settings?.AdressName ?? "API-Testadresse";

                Console.WriteLine($"Stelle Verbindung zu {baseUrl} her ...");

                var client = new NevarisBuildClient(baseUrl);

                // API-Version abfragen und auf Kompatibilität mit Client überprüfen
                var versionCheckResult = await client.CheckVersion();
                if (!versionCheckResult.AreVersionsCompatible)
                {
                    throw new InvalidOperationException($"Versionskonflikt: API: {versionCheckResult.ApiVersion}, client: {versionCheckResult.ClientVersion}");
                }

                // Alle Adressen laden

                Console.WriteLine("Lade Adressen ...");
                var adressen = await client.StammApi.GetAdressen();

                Console.WriteLine($"{adressen.Count} Adressen geladen.");

                // Einzelne Adresse heraussuchen und gegenenfalls neu anlegen

                Console.WriteLine($"Suche Adresse mit Namen '{nameTestAdresse}' ...");

                var testAdresse = adressen.FirstOrDefault(a => a.Name == nameTestAdresse);
                if (testAdresse == null)
                {
                    Console.WriteLine("Adresse nicht gefunden. Lege neue Adresse an.");
                    testAdresse = await client.StammApi.CreateAdresse(new NewAdresseInfo { AdressArt = AdressArt.Organisation });
                    testAdresse.Name = nameTestAdresse; // Namen zuweisen
                    await client.StammApi.UpdateAdresse(testAdresse.Code, testAdresse); // und geänderte Adresse speichern
                    Console.WriteLine($"Adresse mit Code {testAdresse.Code} angelegt.");
                }
                else
                {
                    Console.WriteLine($"Adresse gefunden (Code = {testAdresse.Code}).");

                    // Die Adresse vollständig laden. (Der Aufruf client.StammApi.GetAdressen lädt nämlich nur
                    // die Basiseigenschaften, d.h. ohne Detailinfos wie Bankverbindungen, Adressaten usw.)
                    Console.WriteLine($"Lade vollständige Adresse mit Code = {testAdresse.Code} ...");
                    testAdresse = await client.StammApi.GetAdresse(testAdresse.Code);
                }

                // Ändere einige Eigenschaften

                Console.WriteLine("Ändere einige Eigenschaften der Adresse ...");

                testAdresse.Beschreibung = "Dies ist eine Beispieladresse";
                testAdresse.GültigAb = DateTime.Now;

                if (testAdresse.Bankverbindungen.Count == 0)
                {
                    testAdresse.Bankverbindungen.Add(new Bankverbindung { Bankname = "Berliner Sparkasse", Iban = "DE19123412341234123412", Bic = "BELADEBEXXX" });
                }

                // Speichere die geänderte Adresse

                Console.WriteLine("Aktualisiere Adresse in NEVARIS Build ...");
                await client.StammApi.UpdateAdresse(testAdresse.Code, testAdresse);

                Console.WriteLine("Aktualisierung erfolgreich.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }
        }
    }
}

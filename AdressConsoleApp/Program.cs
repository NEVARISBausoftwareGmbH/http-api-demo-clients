using Nevaris.Build.ClientApi;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdressConsoleApp
{
    /// <summary>
    /// Ein Testprogramm, das den Zugriff auf Adressen über die NEVARIS Build API demonstriert.
    /// </summary>
    class Program
    {
        const string nameTestAdresse = "API-Testadresse";

        static async Task Main(string[] args)
        {
            var client = new NevarisBuildClient("http://localhost:8500");

            // Alle Adressen laden

            Console.WriteLine("Lade Adressen ...");
            var adressen = await client.StammApi.GetAdressen();

            Console.WriteLine($"{adressen.Count} Adressen geladen.");

            // Einzelne Adresse heraussuchen und gegenenfalls neu anlegen

            Console.WriteLine($"Suche Adresse mit Namen {nameTestAdresse} ...");

            var testAdresse = adressen.FirstOrDefault(a => a.Name == nameTestAdresse);
            if (testAdresse == null)
            {
                Console.WriteLine("Adresse nicht gefunden. Lege neue Adresse an.");
                testAdresse = await client.StammApi.CreateAdresse(new NewAdresseInfo { AdressArt = AdressArt.Organisation });
                Console.WriteLine($"Adresse mit Code {testAdresse.Code} angelegt.");
            }
            else
            {
                Console.WriteLine($"Adresse gefunden (Code = {testAdresse.Code}).");
            }

            // Die Adresse vollständig laden. (Der Aufruf client.StammApi.GetAdressen lädt nämlich nur
            // die Basiseigenschaften, d.h. ohne Detailinfos wie Bankverbindungen, Adressaten usw.)

            Console.WriteLine($"Lade vollständige Adresse mit Code = {testAdresse.Code} ...");
            testAdresse = await client.StammApi.GetAdresse(testAdresse.Code);

            // Ändere einige Eigenschaften

            Console.WriteLine("Ändere einige Eigenschaften der Adresse ...");

            testAdresse.Name = nameTestAdresse; // für den Fall, dass die Adresse neu angelegt wurde, müssen wir den Namen noch befüllen
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
    }
}

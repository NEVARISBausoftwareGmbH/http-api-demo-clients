using Nevaris.Build.ClientApi;

// Verbindung zur RESTful API herstellen. Dies setzt voraus, dass auf dem lokalen Rechner
// der NEVARIS Build Businessdienst läuft und die API hostet. Der erste Zugriff erfolgt
// erst beim ersten API-Aufruf (hier: client.StammApi.GetSpeicherorte()).
using var client = new NevarisBuildClient("http://localhost:8500");

// Auslesen der Speicherorte
var speicherorte = await client.StammApi.GetSpeicherorte();

foreach (var speicherort in speicherorte)
{
    Console.WriteLine($"{speicherort.Id}: {speicherort.Bezeichnung}");
}

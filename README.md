# http-api-demo-clients

Diese Visual Studio-Solution enthält Projekte, die den Zugriff auf NEVARIS Build 2022.1 über die RESTful API demonstrieren. Dabei kommt die [Nevaris.Build.ClientApi](https://github.com/NEVARISBausoftwareGmbH/http-api-client-libs) zum Einsatz.

* *BetriebsmittelStammApp:* WPF-Applikation, die Betriebsmittelstämme von einem System in ein anderes kopiert.
* *AdressConsoleApp:* Konsolenapplikation, die den Zugriff auf globale Adressen demonstriert. Die notwendigen Einstellungen (unter anderem die Basis-URL des Zielsystems) müssen in der Datei _Settings.json_ eingetragen werden.
* *AbrechnungConsoleApp:* Konsolenapplikation, die den Lesezugriff auf Abrechnungsdaten (Positionsblöcke mit Aufmaßzeilen) eines Projekts demonstriert. Die Basis-URL des Zielsystems sowie die Informationen zur Projektidentifikation müssen in _Settings.json_ eingetragen werden.

## Voraussetzungen ##

Informationen zur Installation der RESTful API befinden sich hier: [Nevaris.Build.ClientApi](https://github.com/NEVARISBausoftwareGmbH/http-api-client-libs).
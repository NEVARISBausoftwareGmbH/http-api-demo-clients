using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Refit;

namespace Nevaris.Build.ClientApi
{
    /// <summary>
    /// Ermöglicht die Steuerung von NEVARIS Build (Stammdaten und Projekte) über die NEVARIS Build API.
    /// Voraussetzung ist, dass der NEVARIS Build Businessdienst auf einem erreichbaren Server läuft und
    /// so konfiguriert ist, dass die HTTP API bereitgestellt wird. Dazu muss auf dem Server in der
    /// %PROGRAMDATA%/Nemetschek/Nevaris/Nevaris.config ein entsprechender SystemSetting-Eintrag
    /// (&lt;HttpApiBaseAddress&gt;http://*:8500&lt;/HttpApiBaseAddress&gt;) vorhanden sein. Zur Kontrolle,
    /// ob die HTTP API verfügbar ist, kann vom Client aus in einem Browser über die URL [BASIS-URL]/api-docs
    /// die API-Doku abgerufen werden (z.B. http://localhost:8500/api-docs für den Fall, dass der
    /// Businessdienst auf dem lokalen Rechner läuft).
    /// </summary>
    public class NevarisBuildClient
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="hostUrl">Die Basis-URL, auf der die API bereitgestellt wird, z.B. "http://localhost:8500".</param>
        public NevarisBuildClient(string hostUrl)
        {
            HostUrl = hostUrl;
            ProjektApi = RestService.For<IProjektApi>(hostUrl, _refitSettings);
            StammApi = RestService.For<IStammApi>(hostUrl, _refitSettings);
        }

        /// <summary>
        /// Liefert die im Konstruktor übergebene Basis-URL.
        /// </summary>
        public string HostUrl { get; }

        /// <summary>
        /// Zugriff auf projektspezifische Operationen.
        /// </summary>
        public IStammApi StammApi { get; }

        /// <summary>
        /// Zugriff auf Stammdaten-Operationen.
        /// </summary>
        public IProjektApi ProjektApi { get; }

        static RefitSettings _refitSettings = new RefitSettings
        {
            JsonSerializerSettings = _jsonSerializerSettings
        };

        static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = { new StringEnumConverter { CamelCaseText = true } },
            NullValueHandling = NullValueHandling.Ignore
        };
    }
}

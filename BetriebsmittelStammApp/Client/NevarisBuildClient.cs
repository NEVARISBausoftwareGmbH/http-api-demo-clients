using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Refit;

namespace HttpApiClient.Client
{
    public class NevarisBuildClient
    {
        public NevarisBuildClient(string hostUrl)
        {
            HostUrl = hostUrl;
            ProjektApi = RestService.For<IProjektApi>(hostUrl, _refitSettings);
            StammApi = RestService.For<IStammApi>(hostUrl, _refitSettings);
        }

        public string HostUrl { get; }

        public IStammApi StammApi { get; }

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

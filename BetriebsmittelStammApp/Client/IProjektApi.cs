using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace HttpApiClient.Client
{
    public interface IProjektApi
    {
        [Get("/api/projekte/{projektId}")]
        Task<Projekt> GetProjekt(string projektId);

        [Delete("/api/projekte/{projektId}")]
        Task DeleteProjekt(string projektId);

        [Get("/api/projekte/{projektId}/leistungsverzeichnisse")]
        Task<List<Leistungsverzeichnis>> GetLeistungsverzeichnisse(string projektId);

        [Get("/api/projekte/{projektId}/leistungsverzeichnisse/{leistungsverzeichnisId}")]
        Task<Leistungsverzeichnis> GetLeistungsverzeichnis(string projektId, Guid leistungsverzeichnisId);
    }
}

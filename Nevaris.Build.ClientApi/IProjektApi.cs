using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace Nevaris.Build.ClientApi
{
    /// <summary>
    /// Interface zum Zugriff auf projektspezifische Operationen über die NEVARIS Build API.
    /// </summary>
    public interface IProjektApi
    {
        [Get("/build/projekte/{projektId}")]
        Task<Projekt> GetProjekt(string projektId);

        [Delete("/build/projekte/{projektId}")]
        Task DeleteProjekt(string projektId);

        [Get("/build/projekte/{projektId}/leistungsverzeichnisse")]
        Task<List<Leistungsverzeichnis>> GetLeistungsverzeichnisse(string projektId);

        [Get("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}?mitKnoten={mitKnoten}&mitKalkulationen={mitKalkulationen}")]
        Task<Leistungsverzeichnis> GetLeistungsverzeichnis(string projektId, Guid lvId, bool mitKnoten = true, bool mitKalkulationen = true);

        [Post("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/kalkulationen")]
        Task<Kalkulation> CreateKalkulation(string projektId, Guid lvId, [Body] NewKalkulationInfo newKalkulationInfo);

        [Get("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/abrechnungsMerkmale?mengenArt={mengenArt}")]
        Task<IEnumerable<AbrechnungsMerkmal>> GetAbrechnungsMerkmale(string projektId, Guid lvId, MengenArt mengenArt = MengenArt.Abrechnung);

        [Put("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/abrechnungsMerkmale?mengenArt={mengenArt}")]
        Task<AbrechnungsMerkmal> UpdateAbrechnungsMerkmale(string projektId, Guid lvId, [Body] IEnumerable<AbrechnungsMerkmal> abrechnungsMerkmale, MengenArt mengenArt = MengenArt.Abrechnung);

        [Get("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/aufmassblaetter?mengenArt={mengenArt}")]
        Task<IEnumerable<Aufmaßblatt>> GetAufmaßblätter(string projektId, Guid lvId, MengenArt mengenArt = MengenArt.Abrechnung);

        [Put("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/aufmassblaetter?mengenArt={mengenArt}")]
        Task<Aufmaßblatt> UpdateAufmaßblätter(string projektId, Guid lvId, [Body] IEnumerable<Aufmaßblatt> aufmaßblätter, MengenArt mengenArt = MengenArt.Abrechnung);

        [Get("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/rechnungen")]
        Task<IEnumerable<Rechnung>> GetRechnungen(string projektId, Guid lvId);

        [Post("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/rechnungen")]
        Task<Rechnung> CreateRechnung(string projektId, Guid lvId, [Body] NewRechnungInfo newRechnungInfo);

        [Get("/build/projekte/{projektId}/rechnungen/{rechnungId}")]
        Task<Rechnung> GetRechnung(string projektId, Guid rechnungId);

        [Get("/build/projekte/{projektId}/rechnungen/{rechnungId}")]
        Task DeleteRechnung(string projektId, Guid rechnungId);

        [Get("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/positionsbloecke?mengenArt?{mengenArt}&mitZeilen={mitZeilen}&aufmassblattId={aufmassblattId}&leistungszeitraumId={leistungszeitraumId}&rechnungId={rechnungId}")]
        Task<IEnumerable<Positionsblock>> GetPositionsblöcke(
            string projektId,
            Guid lvId,
            MengenArt mengenArt = MengenArt.Abrechnung,
            bool mitZeilen = true,
            Guid? aufmassblattId = null,
            Guid? leistungszeitraumId = null,
            Guid? rechnungId = null);

        [Post("/build/projekte/{projektId}/leistungsverzeichnisse/{lvId}/positionsbloecke")]
        Task<Positionsblock> CreatePositionsblock(string projektId, Guid lvId, [Body]NewPositionsblockInfo newPositionsblockInfo);

        [Get("/build/projekte/{projektId}/positionsbloecke/{positionsblockId}")]
        Task<Positionsblock> GetPositionsblock(string projektId, Guid positionsblockId);

        [Put("/build/projekte/{projektId}/positionsbloecke/{positionsblockId}")]
        Task UpdatePositionsblock(string projektId, Guid positionsblockId, [Body] Positionsblock positionsblock);

        [Delete("/build/projekte/{projektId}/positionsbloecke/{positionsblockId}")]
        Task DeletePositionsblock(string projektId, Guid positionsblockId);

        [Get("/build/projekte/{projektId}/kalkulationen/{kalkulationId}")]
        Task<Kalkulation> GetKalkulation(string projektId, Guid kalkulationId);

        [Post("/build/projekte/{projektId}/kalkulationen/{parentKalkulationId}/kalkulationen")]
        Task<Kalkulation> CreateUntergeordneteKalkulation(string projektId, Guid parentKalkulationId);

        [Put("/build/projekte/{projektId}/kalkulationen/{kalkulationId}")]
        Task UpdateKalkulation(string projektId, Guid kalkulationId, [Body] Kalkulation kalkulation);

        [Delete("/build/projekte/{projektId}/kalkulationen/{kalkulationId}")]
        Task DeleteKalkulation(string projektId, Guid kalkulationId);

        [Get("/build/projekte/{projektId}/kalkulationen/{kalkulationId}/kalkulationsBlaetter?mitZeilen={mitZeilen}")]
        Task<List<KalkulationsBlatt>> GetKalkulationsBlätter(string projektId, Guid kalkulationId, bool mitZeilen = false);

        [Get("/build/projekte/{projektId}/kalkulationen/{kalkulationId}/kalkulationsBlaetter/{positionId}?createNewIfNecessary={createNewIfNecessary}&includeParentKalkulationen={includeParentKalkulationen}")]
        Task<KalkulationsBlatt> GetKalkulationsBlatt(string projektId, Guid kalkulationId, Guid positionId, bool createNewIfNecessary = false, bool includeParentKalkulationen = false);

        [Put("/build/projekte/{projektId}/kalkulationen/{kalkulationId}/kalkulationsBlaetter/{positionId}")]
        Task UpdateKalkulationsBlatt(string projektId, Guid kalkulationId, Guid positionId, [Body] KalkulationsBlatt kalkulationsBlatt);

        [Delete("/build/projekte/{projektId}/kalkulationen/{kalkulationId}/kalkulationsBlaetter/{positionId}")]
        Task DeleteKalkulationsBlatt(string projektId, Guid kalkulationId, Guid positionId);

        [Get("/build/projekte/{projektId}/betriebsmittel?art={art}&mitGruppen={mitGruppen}&mitKosten={mitKosten}&mitWeiterenKosten={mitWeiterenKosten}&mitZuschlaegen={mitZuschlaegen}&mitDetails={mitDetails}&kostenebeneId={kostenebeneId}&zuschlagsebeneId={zuschlagsebeneId}")]
        Task<List<Betriebsmittel>> GetAllBetriebsmittel(
            string projektId,
            BetriebsmittelArt? art,
            bool mitGruppen = true,
            bool mitKosten = false,
            bool mitWeiterenKosten = false,
            bool mitZuschlaegen = false,
            bool mitDetails = false,
            Guid? kostenebeneId = null,
            Guid? zuschlagsebeneId = null);

        [Get("/build/projekte/{projektId}/betriebsmittel/{betriebsmittelId}?art={art}&kostenebeneId={kostenebeneId}&zuschlagsebeneId={zuschlagsebeneId}")]
        Task<Betriebsmittel> GetBetriebsmittel(
            string projektId,
            Guid betriebsmittelId,
            BetriebsmittelArt? art = null,
            Guid? kostenebeneId = null,
            Guid? zuschlagsebeneId = null);

        [Post("/build/projekte/{projektId}/betriebsmittel")]
        Task<Betriebsmittel> CreateBetriebsmittel(string projektId, [Body] NewBetriebsmittelInfo newBetriebsmittelInfo);

        [Put("/build/projekte/{projektId}/betriebsmittel/{betriebsmittelId}")]
        Task UpdateBetriebsmittel(string projektId, Guid betriebsmittelId, [Body] Betriebsmittel betriebsmittel);

        [Delete("/build/projekte/{projektId}/betriebsmittel/{betriebsmittelId}")]
        Task DeleteBetriebsmittel(string projektId, Guid betriebsmittelId);

        [Get("/build/projekte/{projektId}/bautagesberichte/projektdaten")]
        Task<BautagebuchProjektdaten> GetBautagesberichtProjektdaten(string projektId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace HttpApiClient.Client
{
    public interface IStammApi
    {
        [Get("/api/global/speicherorte")]
        Task<List<Speicherort>> GetSpeicherorte();

        [Get("/api/global/speicherorte/{id}")]
        Task<Speicherort> GetSpeicherort(Guid id);

        [Post("/api/global/speicherorte/{speicherortId}/projekte")]
        Task<Projekt> CreateProjekt(Guid speicherortId, [Body] NewProjektInfo newProjekt);

        [Get("/api/global/betriebsmittelstaemme")]
        Task<List<BetriebsmittelStamm>> GetBetriebsmittelStämme();

        [Get("/api/global/betriebsmittelstaemme/{betriebsmittelStammId}")]
        Task<BetriebsmittelStamm> GetBetriebsmittelStamm(Guid betriebsmittelStammId);

        [Post("/api/global/betriebsmittelstaemme")]
        Task<BetriebsmittelStamm> CreateBetriebsmittelStamm([Body] NewBetriebsmittelStammInfo newBetriebsmittelstammInfo);

        [Put("/api/global/betriebsmittelstaemme/{betriebsmittelStammId}")]
        Task UpdateBetriebsmittelStamm(Guid betriebsmittelStammId, [Body] BetriebsmittelStamm betriebsmittelstamm);

        [Delete("/api/global/betriebsmittelstaemme/{betriebsmittelStammId}")]
        Task DeleteBetriebsmittelStamm(Guid betriebsmittelStammId);

        [Get("/api/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel?art={art}&mitGruppen={mitGruppen}&mitKosten={mitKosten}")]
        Task<List<Betriebsmittel>> GetBetriebsmittelList(Guid betriebsmittelStammId, BetriebsmittelArt? art, bool mitGruppen = true, bool mitKosten = false);

        [Get("/api/global/betriebsmittel/{betriebsmittelId}")]
        Task<Betriebsmittel> GetBetriebsmittel(Guid betriebsmittelId, BetriebsmittelArt? art = null);

        [Post("/api/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel")]
        Task<Betriebsmittel> CreateBetriebsmittel(Guid betriebsmittelStammId, [Body] NewBetriebsmittelInfo newBetriebsmittelInfo);

        [Put("/api/global/betriebsmittel/{betriebsmittelId}")]
        Task UpdateBetriebsmittel(Guid betriebsmittelId, [Body] Betriebsmittel betriebsmittel);

        [Delete("/api/global/betriebsmittel/{betriebsmittelId}")]
        Task DeleteBetriebsmittel(Guid betriebsmittelId);
    }
}

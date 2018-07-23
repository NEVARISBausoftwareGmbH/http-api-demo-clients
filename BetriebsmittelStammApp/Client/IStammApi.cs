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
        [Get("/build/global/speicherorte")]
        Task<List<Speicherort>> GetSpeicherorte();

        [Get("/build/global/speicherorte/{id}")]
        Task<Speicherort> GetSpeicherort(Guid id);

        [Post("/build/global/speicherorte/{speicherortId}/projekte")]
        Task<Projekt> CreateProjekt(Guid speicherortId, [Body] NewProjektInfo newProjekt);

        [Get("/build/global/betriebsmittelstaemme")]
        Task<List<BetriebsmittelStamm>> GetBetriebsmittelStämme();

        [Get("/build/global/betriebsmittelstaemme/{betriebsmittelStammId}")]
        Task<BetriebsmittelStamm> GetBetriebsmittelStamm(Guid betriebsmittelStammId);

        [Post("/build/global/betriebsmittelstaemme")]
        Task<BetriebsmittelStamm> CreateBetriebsmittelStamm([Body] NewBetriebsmittelStammInfo newBetriebsmittelstammInfo);

        [Put("/build/global/betriebsmittelstaemme/{betriebsmittelStammId}")]
        Task UpdateBetriebsmittelStamm(Guid betriebsmittelStammId, [Body] BetriebsmittelStamm betriebsmittelstamm);

        [Delete("/build/global/betriebsmittelstaemme/{betriebsmittelStammId}")]
        Task DeleteBetriebsmittelStamm(Guid betriebsmittelStammId);

        [Get("/build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel?art={art}&mitGruppen={mitGruppen}&mitKosten={mitKosten}&mitWeiterenKosten={mitWeiterenKosten}&mitZuschlaegen={mitZuschlaegen}&mitDetails={mitDetails}")]
        Task<List<Betriebsmittel>> GetAllBetriebsmittel(
            Guid betriebsmittelStammId,
            BetriebsmittelArt? art,
            bool mitGruppen = true,
            bool mitKosten = false,
            bool mitWeiterenKosten = false,
            bool mitZuschlaegen = false,
            bool mitDetails = false);

        [Get("/build/global/betriebsmittel/{betriebsmittelId}")]
        Task<Betriebsmittel> GetBetriebsmittel(Guid betriebsmittelId, BetriebsmittelArt? art = null);

        [Post("/build/global/betriebsmittelstaemme/{betriebsmittelStammId}/betriebsmittel")]
        Task<Betriebsmittel> CreateBetriebsmittel(Guid betriebsmittelStammId, [Body] NewBetriebsmittelInfo newBetriebsmittelInfo);

        [Put("/build/global/betriebsmittel/{betriebsmittelId}")]
        Task UpdateBetriebsmittel(Guid betriebsmittelId, [Body] Betriebsmittel betriebsmittel);

        [Delete("/build/global/betriebsmittel/{betriebsmittelId}")]
        Task DeleteBetriebsmittel(Guid betriebsmittelId);
    }
}

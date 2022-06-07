using Lv_Viewer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApi_Wpf_Bommhardt
{
    public class LvPosition : LvItem
    {
        public LvPosition(Nevaris.Build.ClientApi.LvPosition? lvPosition, MengenArtViewItem? mengenArt) 
            : base(lvPosition)
        {            
            Menge = lvPosition?.LvMenge;
            Einheit = lvPosition?.Einheit;
            Einheitspreis = lvPosition?.Ergebnisse?.Einheitspreis?.FirstValue;
            _itemType = lvPosition?.ItemTyp;
            _mengenArt = mengenArt;            
        }

        private MengenArtViewItem? _mengenArt;
        private Nevaris.Build.ClientApi.LvItemTyp? _itemType;

        private bool IsMengenPosition(Nevaris.Build.ClientApi.LvItemTyp? lvItemTyp) =>
            lvItemTyp == Nevaris.Build.ClientApi.LvItemTyp.GaebLeistungsposition ||
                lvItemTyp == Nevaris.Build.ClientApi.LvItemTyp.GaebZuschlagsposition ||
                lvItemTyp == Nevaris.Build.ClientApi.LvItemTyp.GaebUnterbeschreibung;

        public decimal? Menge { get; set; }
        public string? Einheit { get; set; }
        public decimal? Einheitspreis { get; set; }
        public string? MengenArt => IsMengenPosition(_itemType) ? $"{_mengenArt?.Bezeichnung}:" : null;
        public string? EinheitText => IsMengenPosition(_itemType) ? "Eh:" : null;
        public string? EinheitspreisText => IsMengenPosition(_itemType) ? "EP:" : null;
    }
}

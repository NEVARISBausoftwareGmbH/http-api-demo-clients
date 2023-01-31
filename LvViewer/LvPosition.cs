namespace Lv_Viewer
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
        private readonly Nevaris.Build.ClientApi.LvItemTyp? _itemType;

        private bool IsMengenPosition(Nevaris.Build.ClientApi.LvItemTyp? lvItemTyp) =>
            lvItemTyp == Nevaris.Build.ClientApi.LvItemTyp.GaebLeistungsposition ||
                lvItemTyp == Nevaris.Build.ClientApi.LvItemTyp.GaebZuschlagsposition ||
                lvItemTyp == Nevaris.Build.ClientApi.LvItemTyp.GaebUnterbeschreibung;

        public decimal? Menge { get; set; }
        public string? Einheit { get; set; }
        public decimal? Einheitspreis { get; set; }
        public string? MengenArt => IsMengenPosition(_itemType) ? "LV-Menge" : null;
        public string? EinheitText => IsMengenPosition(_itemType) ? "Eh:" : null;
        public string? EinheitspreisText => IsMengenPosition(_itemType) ? "EP:" : null;
    }
}

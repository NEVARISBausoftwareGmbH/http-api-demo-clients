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
        public LvPosition(Nevaris.Build.ClientApi.LvPosition? lvPosition) 
            : base(lvPosition)
        {
            Menge = lvPosition?.LvMenge;
            Einheit = lvPosition?.Einheit;
            Einheitspreis = lvPosition?.Ergebnisse?.Einheitspreis?.FirstValue;
        }

        public decimal? Menge { get; set; }
        public string? Einheit { get; set; }
        public decimal? Einheitspreis { get; set; }
    }
}

using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lv_Viewer
{
    public class MengenArtViewItem
    {
        public MengenArtViewItem(MengenArt art)
        {
            Art = art;
            Bezeichnung = GetBezeichnung(art);
        }

        private string? GetBezeichnung(MengenArt art)
            =>
            art switch
            {                
                MengenArt.Abrechnung => "Abrechnungsmenge",
                MengenArt.Rechnung => "Rechnungsmenge",
                MengenArt.AbrechnungKorrigiert => "Korrigierte Abrechnungsmenge",
                MengenArt.Rechnungkorrigiert => "Korrigierte Rechnungsmenge",
                MengenArt.Umlagemenge => "Umlagemenge",
                MengenArt.Prognose1 => "Prognosemenge 1",
                MengenArt.Prognose2 => "Prognosemenge 2",
                MengenArt.Prognose3 => "Prognosemenge 3",
                MengenArt.Prognose4 => "Prognosemenge 4",
                MengenArt.Prognose5 => "Prognosemenge 5",
                MengenArt.Prognose6 => "Prognosemenge 6",
                MengenArt.Prognose7 => "Prognosemenge 7",
                MengenArt.Prognose8 => "Prognosemenge 8",
                MengenArt.Prognose9 => "Prognosemenge 9",
                MengenArt.Prognose10 => "Prognosemenge 10",
                _ => "LV-Menge"
            };

        public MengenArt? Art { get; private set; }
        public string? Bezeichnung { get; private set; }
        public static List<MengenArt> Arten = new() 
        {
            MengenArt.Lv, 
            MengenArt.Abrechnung, 
            MengenArt.AbrechnungKorrigiert,
            MengenArt.Rechnung,
            MengenArt.Rechnungkorrigiert,
            MengenArt.Umlagemenge,
            MengenArt.Prognose1,
            MengenArt.Prognose2,
            MengenArt.Prognose3,
            MengenArt.Prognose4,
            MengenArt.Prognose5,
            MengenArt.Prognose6,
            MengenArt.Prognose7,
            MengenArt.Prognose8,
            MengenArt.Prognose9,
            MengenArt.Prognose10
        };        
    }
}

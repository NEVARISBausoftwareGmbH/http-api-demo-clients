using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApi_Wpf_Bommhardt
{
    public class LvItem : NotifyPropertyChangedBase
    {
        public LvItem(LvItemBase? lvItemBase)
        {
            Nummer = lvItemBase?.NummerKomplett;
            Bezeichnung = GetText(lvItemBase?.Stichwort ?? lvItemBase?.FormatierteTexte?.Kurztext);
            Langtext = GetText(lvItemBase?.FormatierteTexte?.Langtext, false);

            //Wenn keine Anzeige möglich in der Liste dann Teile vom Langtext nehmen.
            if (Nummer == null && Bezeichnung == null)
            {
                NummerUndBezeichnung = Langtext;
                if (Langtext?.Length > 60)
                {
                    NummerUndBezeichnung = Langtext?.PadLeft(60);
                }                
            }
            else if (Nummer != null && Bezeichnung != null)
            {
                NummerUndBezeichnung = $"{Nummer} - {Bezeichnung}";
            }
            else if (Nummer == null)
            {
                NummerUndBezeichnung = Bezeichnung;
            }
            else if (Bezeichnung == null)
            {
                NummerUndBezeichnung = Nummer;
            }
        }

        private string? GetText(string? text, bool firstLineOnly = true)
        {
            if (string.IsNullOrEmpty(text)) { return null; }

            var splitPattern = new char[] { '<', '>', '/' };
            var split = text.Split(splitPattern, StringSplitOptions.RemoveEmptyEntries);
            if (split?.Length == 1) { return split[0]; }            
            else if (split?.Length > 1)
            {
                StringBuilder sb = new();
                bool startLuecke =false;
                foreach (var textSegment in split)
                {
                    var trimmedText = textSegment.Trim();
                    if (!trimmedText.Equals("p") && !trimmedText.Equals("br") && !trimmedText.Equals("b"))
                    {
                        if (trimmedText.Equals("al"))
                        {
                            if (!startLuecke)
                            {
                                trimmedText = "<Lücke>";
                                startLuecke = true;
                            }
                            else
                            {
                                trimmedText = "</Lücke>";
                                startLuecke = false;
                            }
                        }
                        sb.AppendLine(trimmedText);

                        if (firstLineOnly)
                            break;
                    }
                }
                return sb.ToString().Trim();
            }
            return null;
        }

        public ObservableCollection<LvItem> ItemNodes { get; set; } = new();
        private string? Nummer { get; set; }
        private string? Bezeichnung { get; set; }
        public string? NummerUndBezeichnung { get; set; }
        public string? Langtext { get; set; }
    }
}

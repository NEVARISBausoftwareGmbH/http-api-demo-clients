using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApi_Wpf_Bommhardt
{
    public class LvItem 
    {
        public LvItem(LvItemBase? lvItemBase)
        {
            Nummer = lvItemBase?.NummerKomplett;
            Bezeichnung = GetText(lvItemBase?.Stichwort ?? lvItemBase?.FormatierteTexte?.Kurztext);
            Langtext = GetText(lvItemBase?.FormatierteTexte?.Langtext, false);
        }

        private string? GetText(string? text, bool firstLineOnly = true)
        {
            if (string.IsNullOrEmpty(text)) { return null; }

            var splitPattern = new char[] { '<', '>', '/' };
            var split = text.Split(splitPattern, StringSplitOptions.RemoveEmptyEntries);
            if (split?.Length == 1) { return split[0]; }
            else if (split?.Length > 1 && firstLineOnly) { return split[1]; }
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
                    }
                }
                return sb.ToString();
            }
            return null;
        }

        public ObservableCollection<LvItem> ItemNodes { get; set; } = new();
        public string? Nummer { get; set; }
        public string? Bezeichnung { get; set; }
        public string? Langtext { get; set; }
    }
}

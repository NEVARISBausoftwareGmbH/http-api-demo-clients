using System;
using System.Collections.ObjectModel;
using System.Text;
using Nevaris.Build.ClientApi;

namespace Lv_Viewer
{
    public class LvItem : NotifyPropertyChangedBase
    {
        private readonly int _numberLength = 60;

        protected LvItem(LvItemBase? lvItemBase)
        {
            Nummer = lvItemBase?.NummerKomplett;
            if (Nummer != null)
                _numberLength  = Nummer.Length;
            Bezeichnung = GetText(lvItemBase?.Stichwort ?? lvItemBase?.FormatierteTexte?.Kurztext);
            var langtext = GetText(lvItemBase?.FormatierteTexte?.Langtext, false);
            FormattedLangtext = lvItemBase?.FormatierteTexte?.Langtext;
            //Wenn keine Anzeige möglich in der Liste dann Teile vom Langtext nehmen.
            if (Nummer == null && Bezeichnung == null)
            {
                NummerUndBezeichnung = langtext;
                if (langtext?.Length > 60)
                {
                    NummerUndBezeichnung = langtext?.PadLeft(60);
                }                
            }
            else if (Nummer != null && Bezeichnung != null)
            {
                NummerUndBezeichnung = $"{Nummer} - {Bezeichnung}";
            }
            else if (Nummer == null)
            {
                NummerUndBezeichnung = "".PadRight(_numberLength + 10) + Bezeichnung;
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
        public string? FormattedLangtext { get; set; }
    }
}

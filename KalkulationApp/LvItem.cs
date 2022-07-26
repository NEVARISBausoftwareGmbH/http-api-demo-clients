using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalkulationApp
{
    public class LvItem : NotifyPropertyChangedBase
    {
        public static int _numberLength = 60;
        public LvItem(LvItemBase? lvItemBase)
        {
            Nummer = lvItemBase?.NummerKomplett;
            if (Nummer != null)
                _numberLength  = Nummer.Length;
            Bezeichnung = GetText(lvItemBase?.Stichwort ?? lvItemBase?.FormatierteTexte?.Kurztext);
            
            if (Nummer != null && Bezeichnung != null)
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

        public LvItem? Search(Func<LvItem, bool> predicate)
        {            
            if (ItemNodes.Count == 0)
            {
                if (predicate(this))
                {
                    IsVisible = true;
                    return this;
                }
                else
                {
                    IsVisible = false;
                    return null;
                }
            }
            else 
            {
                List<LvItem> results = ItemNodes
                                  .Select(i => i.Search(predicate))
                                  .Where(i => i != null).ToList()!;

                if (results.Any())
                {
                    IsVisible = true;
                    var result = (LvItem)MemberwiseClone();
                    result.ItemNodes = new ObservableCollection<LvItem>(results);
                    return result;
                }
                IsVisible = false;
                return null;
            }
        }

        public ObservableCollection<LvItem> ItemNodes { get; set; } = new();
        public string? Nummer { get; private set; }
        public string? Bezeichnung { get; private set; }
        public string? NummerUndBezeichnung { get; set; }

        private bool _isvisible = true;

        public bool IsVisible
        {
            get { return _isvisible; }
            set { _isvisible = value; OnPropertyChanged(nameof(IsVisible)); }
        }

    }
}

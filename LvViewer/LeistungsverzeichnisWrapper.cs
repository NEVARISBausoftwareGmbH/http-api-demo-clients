using Lv_Viewer;
using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApi_Wpf_Bommhardt
{
    public class LeistungsverzeichnisWrapper : NotifyPropertyChangedBase
    {
        private Leistungsverzeichnis _lv;
        private MengenArtViewItem _mengenArt;
        public LeistungsverzeichnisWrapper(Leistungsverzeichnis lv, MengenArtViewItem? mengenArt)
        {
            _lv = lv ?? throw new ArgumentNullException(nameof(lv));
            _mengenArt = mengenArt ?? throw new ArgumentNullException(nameof(mengenArt));

            RefreshUI();

            LoadItemNodes();            
        }        

        private void LoadItemNodes()
        {
            RootNodes.Clear();
            foreach (var item in _lv.RootKnotenListe)
            {
                var rootLvItem = new LvNode(item);
                RootNodes.Add(rootLvItem);
                LoadNodesRecursive(item, rootLvItem);                
            }            
        }

        private void LoadNodesRecursive(LvKnoten? root, LvNode rootLvItem)
        {
            if (root == null) { return; }
            foreach (var node in root.Knoten)
            {
                var nodeLvItem = new LvNode(node);
                rootLvItem.ItemNodes.Add(nodeLvItem);

                foreach (var pos in node.Positionen)
                {
                    var position = new LvPosition(pos, _mengenArt);
                    nodeLvItem.ItemNodes.Add(position);                    
                }

                LoadNodesRecursive(node, nodeLvItem);
            }            
        }        

        private ObservableCollection<LvNode> _rootNodes = new();

        public ObservableCollection<LvNode> RootNodes
        {
            get { return _rootNodes; }
            set { _rootNodes = value; OnPropertyChanged(nameof(RootNodes)); }
        }

        private void RefreshUI()
        {
            OnPropertyChanged(nameof(Umsatzsteuer));
            OnPropertyChanged(nameof(Waehrung));
            OnPropertyChanged(nameof(PlainLangtext));
            OnPropertyChanged(nameof(FormattedLangtext));
        }

        public string? Waehrung => _lv.LvDetails.Währung;
        public string? Umsatzsteuer => _lv.LvDetails.Umsatzsteuer;
        public string? PlainLangtext => SelectedLvItem?.Langtext;
        private string? _formattedLangtext;

        public string? FormattedLangtext
        {
            get { return _formattedLangtext; }
            set { _formattedLangtext = value; OnPropertyChanged(nameof(FormattedLangtext)); }
        }

        private LvItem? _selectedLvItem;

        public LvItem? SelectedLvItem
        {
            get { return _selectedLvItem; }
            set 
            {
                _selectedLvItem = value; 
                OnPropertyChanged(nameof(SelectedLvItem));
                OnPropertyChanged(nameof(PlainLangtext));
            }
        }

        internal void Dispose()
        {
            RootNodes.Clear();
            SelectedLvItem = null;
            RefreshUI();
        }
    }
}

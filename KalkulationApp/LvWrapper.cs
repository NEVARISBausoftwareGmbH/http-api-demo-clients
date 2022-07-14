using Lv_Viewer;
using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KalkulationApp
{
    public class LvWrapper : NotifyPropertyChangedBase
    {
        private Leistungsverzeichnis? _lv;
        public LvWrapper(Leistungsverzeichnis lv)
        {
            _lv = lv ?? throw new ArgumentNullException(nameof(lv));

            LoadItemNodes();
        }        

        private void LoadItemNodes()
        {
            RootNodes.Clear();
            if (_lv == null) { return; }

            var lvRootNode = CreateLvNode();

            foreach (var item in _lv.RootKnotenListe)
            {
                var rootNode = new LvNode(item);
                lvRootNode.ItemNodes.Add(rootNode);

                LoadNodesRecursive(item, rootNode);
            }
        }

        private LvNode CreateLvNode()
        {
            //LV Knoten Element
            LvNode? lvRootNodeItem = null;            
            
            if (_lv != null)
            {
                lvRootNodeItem = new(null);
                lvRootNodeItem.NummerUndBezeichnung = String.Join(" - ", _lv.Nummer, _lv.Bezeichnung);
                RootNodes.Add(lvRootNodeItem);
                //Texte auf LV Ebene
                if (_lv != null)
                {
                    foreach (var pos in _lv.RootPositionen)
                    {
                        var position = new LvPosition(pos);
                        lvRootNodeItem.ItemNodes.Add(position);
                        LvPositionen.Add(position);
                    }
                }
            }
            return lvRootNodeItem!;
        }

        private void LoadNodesRecursive(LvKnoten? root, LvNode rootLvItem)
        {
            if (root == null) { return; }
            foreach (var node in root.Knoten)
            {
                var nodeLvItem = new LvNode(node);
                rootLvItem.ItemNodes.Add(nodeLvItem);
                if (_lv?.Norm == Norm.Gaeb || _lv?.Norm == Norm.Frei)
                {
                    //In der Gaeb kommen die Positionen, wenn vorhanden samt Knoten vor den Knoten.
                    CreatePositionen(node, nodeLvItem);
                    LoadNodesRecursive(node, nodeLvItem);
                }
                else if (_lv?.Norm == Norm.Oenorm)
                {
                    LoadNodesRecursive(node, nodeLvItem);
                    CreatePositionen(node, nodeLvItem);
                }
            }
        }

        private void CreatePositionen(LvKnoten node, LvNode nodeLvItem)
        {
            foreach (var pos in node.Positionen)
            {
                var position = new LvPosition(pos);
                nodeLvItem.ItemNodes.Add(position);
                LvPositionen.Add(position);
            }
        }

        private ObservableCollection<LvNode> _rootNodes = new();

        public ObservableCollection<LvNode> RootNodes
        {
            get { return _rootNodes; }
            set { _rootNodes = value; OnPropertyChanged(nameof(RootNodes)); }
        }
        
        private LvItem? _selectedLvItem;

        public LvItem? SelectedLvItem
        {
            get { return _selectedLvItem; }
            set 
            {
                _selectedLvItem = value; 
                OnPropertyChanged(nameof(SelectedLvItem));
            }
        }

        private string? _searchText;

        public string? SearchText
        {
            get { return _searchText; }
            set 
            {
                _searchText = value; 
                OnPropertyChanged(nameof(SearchText));
                RootNodes.FirstOrDefault()?.
                    Search(i => i.NummerUndBezeichnung?.ToLower()?.Contains(value ?? "") == true);               
            }
        }

        public HashSet<LvPosition> LvPositionen { get; set; } = new();

        internal void Dispose()
        {
            RootNodes.Clear();
            SelectedLvItem = null;
            _lv = null;
        }
    }    
}

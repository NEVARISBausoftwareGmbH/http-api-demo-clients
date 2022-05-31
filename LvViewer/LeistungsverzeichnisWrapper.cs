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
        public LeistungsverzeichnisWrapper(Leistungsverzeichnis lv)
        {
            _lv = lv ?? throw new ArgumentNullException(nameof(lv));
            
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
                    var position = new LvPosition(pos);
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

        internal void Dispose()
        {
            RootNodes.Clear();
        }
    }
}

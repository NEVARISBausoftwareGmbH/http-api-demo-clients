using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalkulationApp
{
    public class Kalkulationsblatt : NotifyPropertyChangedBase
    {        
        public Kalkulationsblatt(KalkulationsBlatt? kalkblatt)
        {
            OrigianlKalkblatt = kalkblatt;

            OnPropertyChanged(nameof(PositionId));
        }

        public void GenerateKalkulationsZeilen()
        {
            if (OrigianlKalkblatt != null)
            {
                foreach (var zeile in OrigianlKalkblatt.Zeilen)
                {
                    var newKalkZeile = new KalkZeile(zeile);
                    newKalkZeile.Kalkblatt = this;
                    newKalkZeile.Generatezeile();
                    var indexInsert = Zeilen.Count - newKalkZeile.Childs.Count;
                    Zeilen.Insert(indexInsert, newKalkZeile);
                }
            }            
        }

        public KalkulationsBlatt? OrigianlKalkblatt { get; private set; }

        private ObservableCollection<KalkZeile> _zeilen = new();

        public ObservableCollection<KalkZeile> Zeilen
        {
            get { return _zeilen; }
            set { _zeilen = value; OnPropertyChanged(nameof(Zeilen)); }
        }

        public Guid? PositionId => OrigianlKalkblatt?.PositionId;

        public ViewModel? Model { get; set; }

        internal Betriebsmittel? GetBetriebsmittel(Guid betriebsmittelId)
        {
            if (Model?.CurrentProjektBetriebsmittel.TryGetValue(betriebsmittelId, out var betriebsmittel) == true)
            {
                return betriebsmittel;
            }
            return null;
        }

        internal bool? MustUpdateKalkblatt { get; set; }

        internal void GenerateUpZeile(KalkulationsZeile upZeile)
        {
            var newKalkZeile = new KalkZeile(upZeile)
            {
                Kalkblatt = this
            };
            newKalkZeile.Generatezeile();            
        }
    }
}

using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HttpApi_Wpf_Bommhardt
{
    public class ViewModel : NotifyPropertyChangedBase
    {
        private MainWindow _mainWindow;
        private NevarisBuildClient Client { get; set; }

        public ViewModel(MainWindow mainView)
        {
            _mainWindow = mainView;
            InitClient();
        }

        private void InitClient()
        {
            Client = new NevarisBuildClient("http://localhost:8500");

            Run();
        }

        private async void Run()
        {
            IEnumerable<Speicherort>? speicherOrte = null;
            try
            {
                speicherOrte = (await Client.StammApi.GetSpeicherorte()).Where(_ => _.DatenbankInfo != null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            

            SpeicherOrte.Clear();
            if (speicherOrte != null)
            {
                foreach (var sp in speicherOrte)
                {
                    SpeicherOrte.Add(sp);
                }
            }
        }

        public void Dispose()
        {
            Client.Dispose();
        }

        private ObservableCollection<Speicherort?> _SpeicherOrte = new();        

        public ObservableCollection<Speicherort?> SpeicherOrte
        {
            get { return _SpeicherOrte; }
            set { _SpeicherOrte = value; OnPropertyChanged(nameof(SpeicherOrte)); }
        }

        private Speicherort? _selectedSpeicherOrt;
                
        public Speicherort? SelectedSpeicherOrt
        {
            get { return _selectedSpeicherOrt; }
            set 
            {
                _selectedSpeicherOrt = value;
                LoadProjekte();
                OnPropertyChanged(nameof(SelectedSpeicherOrt)); 
            }
        }

        private async void LoadProjekte()
        {
            Projekte.Clear();
            Lvs.Clear();
            if (SelectedSpeicherOrt != null)
            {
                Speicherort? speicherort = null;
                try
                {
                    speicherort = await Client.StammApi.GetSpeicherort(SelectedSpeicherOrt.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (speicherort != null)
                {
                    foreach (var p in speicherort.ProjektInfos.OrderBy(_ => _.Nummer).ThenBy(_ => _.Bezeichnung))
                    {
                        Projekte.Add(p);
                    }
                }
            }
        }        

        private ObservableCollection<ProjektInfo> _projekte = new();

        public ObservableCollection<ProjektInfo> Projekte
        {
            get { return _projekte; }
            set { _projekte = value; OnPropertyChanged(nameof(Projekte)); }
        }

        private ProjektInfo _selectedProjekt;

        public ProjektInfo SelectedProjekt
        {
            get { return _selectedProjekt; }
            set 
            { 
                _selectedProjekt = value;
                LoadLvs();
                OnPropertyChanged(nameof(SelectedProjekt)); 
            }
        }

        private async void LoadLvs()
        {
            _mainWindow.SetWaitSpinner(false);
            Lvs.Clear();           
            if (SelectedProjekt != null)
            {
                Projekt? projekt = null;
                try
                {
                    projekt = await Client.ProjektApi.GetProjekt(SelectedProjekt.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

                if (projekt != null)
                {
                    foreach (var lv in projekt.Leistungsverzeichnisse.OrderBy(_ => _.Nummer).ThenBy(_ => _.Bezeichnung))
                    {
                        Lvs.Add(lv);
                    }
                }
            }            
        }

        private ObservableCollection<Leistungsverzeichnis> _lvs = new();

        public ObservableCollection<Leistungsverzeichnis> Lvs
        {
            get { return _lvs; }
            set { _lvs = value; OnPropertyChanged(nameof(Lvs)); }
        }

        private Leistungsverzeichnis _selectedLv;

        public Leistungsverzeichnis SelectedLv
        {
            get { return _selectedLv; }
            set 
            {
                _selectedLv = value;
                OnPropertyChanged(nameof(SelectedLv));
                LoadLv(value);                
            }
        }
        private LeistungsverzeichnisWrapper? _lvDetails;

        public LeistungsverzeichnisWrapper? LvDetails
        {
            get { return _lvDetails; }
            set { _lvDetails = value; OnPropertyChanged(nameof(LvDetails)); }
        }

        private async void LoadLv(Leistungsverzeichnis lv)
        {
            LvDetails?.Dispose();
            _mainWindow.ResetText();
            _mainWindow.SetWaitSpinner(true);
            if (lv != null && SelectedProjekt != null)
            {                
                Leistungsverzeichnis? newLv = null;
                try
                {
                    newLv = await Client.ProjektApi.GetLeistungsverzeichnis(SelectedProjekt.Id, lv.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    _mainWindow.SetWaitSpinner(false);
                }

                if (newLv != null)
                {
                    LvDetails = new LeistungsverzeichnisWrapper(newLv);
                    _mainWindow.SetTreeViewSource();                    
                }
            }            
        }
    }
}

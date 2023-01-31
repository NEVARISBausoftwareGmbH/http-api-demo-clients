using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Nevaris.Build.ClientApi;

namespace Lv_Viewer
{
    public class ViewModel : NotifyPropertyChangedBase
    {
        private readonly MainWindow _mainWindow;

        private NevarisBuildClient? Client { get; set; }

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
                if (Client != null)
                {
                    speicherOrte = (await Client.StammApi.GetSpeicherorte())?.Where(_ => _.DatenbankInfo != null);
                }
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
            Client?.Dispose();
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
            if (SelectedSpeicherOrt != null && Client != null)
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

        private ProjektInfo? _selectedProjekt;

        public ProjektInfo? SelectedProjekt
        {
            get { return _selectedProjekt; }
            set 
            { 
                _selectedProjekt = value;
                LoadLvs();
                OnPropertyChanged(nameof(SelectedProjekt)); 
            }
        }

        private MengenArtViewItem? _selectedMenge;

        public MengenArtViewItem? SelectedMenge
        {
            get { return _selectedMenge; }
            set 
            { 
                _selectedMenge = value; 
                OnPropertyChanged(nameof(SelectedMenge));
                //LV mit neuer Menge neu laden.
                LoadLv(SelectedLv);
            }
        }

        private ObservableCollection<MengenArtViewItem> _mengen = new();

        public ObservableCollection<MengenArtViewItem> Mengen
        {
            get { return _mengen; }
            set { _mengen = value; OnPropertyChanged(nameof(Mengen)); }
        }

        private void LoadMengen()
        {
            if (Mengen.Count > 0) { return; }
            Mengen.Clear();
            foreach (var art in MengenArtViewItem.Arten)
            {
                Mengen.Add(new MengenArtViewItem(art));
            }
            _selectedMenge = Mengen.First(_ => _.Art == MengenArt.Lv);
            OnPropertyChanged(nameof(SelectedMenge));
        }

        private async void LoadLvs()
        {
            _mainWindow.SetWaitSpinner(false);
            Lvs.Clear();           
            if (SelectedProjekt != null && Client != null)
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

        private Leistungsverzeichnis? _selectedLv;

        public Leistungsverzeichnis? SelectedLv
        {
            get { return _selectedLv; }
            set 
            {
                _selectedLv = value;
                OnPropertyChanged(nameof(SelectedLv));               
                LoadLv(value);
                LoadMengen();
            }
        }
        private LeistungsverzeichnisWrapper? _lvDetails;

        public LeistungsverzeichnisWrapper? LvDetails
        {
            get { return _lvDetails; }
            set { _lvDetails = value; OnPropertyChanged(nameof(LvDetails)); }
        }

        private async void LoadLv(Leistungsverzeichnis? lv)
        {
            LvDetails?.Dispose();
            _mainWindow.ClearFormattedText();
            if (lv != null && SelectedProjekt != null && Client != null)
            {                
                _mainWindow.SetWaitSpinner(true);                
                Leistungsverzeichnis? newLv = null;
                try
                {
                    newLv = await Client.ProjektApi.GetLeistungsverzeichnis
                        (SelectedProjekt.Id, lv.Id, SelectedMenge?.Art ?? MengenArt.Lv);
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
                    LvDetails = new LeistungsverzeichnisWrapper(newLv, SelectedMenge);
                    _mainWindow.SetTreeViewSource();                    
                }
            }            
        }
    }
}

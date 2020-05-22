using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Nevaris.Build.ClientApi;

namespace BetriebsmittelStammApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _quellHostUrlTextBox.Focus();
        }

        NevarisBuildClient _quellClient, _zielClient;

        public NevarisBuildClient QuellClient
        {
            get
            {
                if (_quellClient == null || _quellClient.HostUrl != _quellHostUrlTextBox.Text)
                {
                    _quellClient = new NevarisBuildClient(_quellHostUrlTextBox.Text);
                }

                return _quellClient;
            }
        }

        public NevarisBuildClient ZielClient
        {
            get
            {
                if (string.IsNullOrEmpty(_zielHostUrlTextBox.Text))
                {
                    return QuellClient;
                }

                if (_zielClient == null || _zielClient.HostUrl != _zielHostUrlTextBox.Text)
                {
                    _zielClient = new NevarisBuildClient(_zielHostUrlTextBox.Text);
                }

                return _zielClient;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        List<BetriebsmittelStammViewItem> _quellStammViewModels;

        public List<BetriebsmittelStammViewItem> QuellStammViewModels
        {
            get { return _quellStammViewModels; }
            set { _quellStammViewModels = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuellStammViewModels))); }
        }

        public BetriebsmittelStammViewItem QuellStammViewModel { get; set; }

        public string ZielStammBezeichnung { get; set; }

        bool _controlsAreEnabled = true;

        public bool ControlsAreEnabled
        {
            get { return _controlsAreEnabled; }
            set { _controlsAreEnabled = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ControlsAreEnabled))); }
        }

        async void OnBetriebsmittelstämmeLaden(object sender, RoutedEventArgs e)
        {
            _meldungenParagraph.Inlines.Clear();

            try
            {
                QuellStammViewModels = (await QuellClient.StammApi.GetBetriebsmittelStämme()).Select(stamm => new BetriebsmittelStammViewItem(stamm)).OrderBy(v => v.VollständigeBezeichnung).ToList();

                if (QuellStammViewModels.Count > 0)
                {
                    _quellStämmeComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                LogInfoText(ex.Message, true);
            }
        }

        async void OnBetriebsmittelstämmeKopieren(object sender, RoutedEventArgs e)
        {
            _meldungenParagraph.Inlines.Clear();

            if (QuellStammViewModel == null)
            {
                LogInfoText("Bitte wählen Sie einen Quellstamm aus.", true);
                return;
            }

            if (string.IsNullOrEmpty(ZielStammBezeichnung))
            {
                LogInfoText("Bitte geben Sie einen Namen für den Zielstamm ein.", true);
                return;
            }

            Stopwatch sw = Stopwatch.StartNew();

            LogInfoText($"Der Stamm '{QuellStammViewModel.VollständigeBezeichnung}' wird nach '{ZielStammBezeichnung}' kopiert.");
            ControlsAreEnabled = false;

            try
            {
                await BetriebsmittelStammUtils.BetriebsmittelstammKopieren(
                    QuellClient.StammApi,
                    ZielClient.StammApi,
                    QuellStammViewModel.Model.Id,
                    ZielStammBezeichnung,
                    args => LogInfoText(args.Message));

                LogInfoText($"Der Stamm '{QuellStammViewModel.VollständigeBezeichnung}' wurde erfolgreich nach '{ZielStammBezeichnung}' kopiert.");
            }
            catch (Exception ex)
            {
                LogInfoText(ex.Message, true);
                return;
            }
            finally
            {
                // Gib die benötigte Zeit im Format d.hh:mm:ss aus
                TimeSpan span = sw.Elapsed;
                string elapsed = new TimeSpan(span.Ticks - span.Ticks % TimeSpan.TicksPerSecond).ToString(@"d\.hh\:mm\:ss");
                LogInfoText($"Benötigte Zeit (d.hh:mm:ss): {elapsed}");
                ControlsAreEnabled = true;
            }
        }

        public void LogInfoText(string text, bool isError = false, string caption = null)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var span = new Span();

                if (isError)
                {
                    span.Foreground = Brushes.Red;
                }

                if (string.IsNullOrEmpty(caption))
                {
                    span.Inlines.Add(new Bold(new Run(text)));
                }
                else
                {
                    span.Inlines.Add(new Italic(new Run(caption + ": ")));
                    span.Inlines.Add(text);
                }

                _meldungenParagraph.Inlines.Add(span);
                _meldungenParagraph.Inlines.Add(new LineBreak());

                while (_meldungenParagraph.Inlines.Count > 100) // begrenze Anzahl an Meldungen
                {
                    _meldungenParagraph.Inlines.Remove(_meldungenParagraph.Inlines.FirstInline);
                }

                _meldungenBox.ScrollToEnd();
            }));
        }
    }

    public class BetriebsmittelStammViewItem
    {
        public BetriebsmittelStammViewItem(BetriebsmittelStamm model)
        {
            Model = model;
        }

        public BetriebsmittelStamm Model { get; }

        public string VollständigeBezeichnung => string.Join(" – ", new[] { Model.Nummer, Model.Bezeichnung }.Where(s => !string.IsNullOrEmpty(s)));
    }
}

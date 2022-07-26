using System;
using System.Collections.Generic;
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

namespace KalkulationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Model = new(this);
        }

        public void SetTreeViewSource()
            => Tv.ItemsSource = Model?.LvDetails?.RootNodes;

        public void ResetTreeViewSource()
            => Tv.ItemsSource = null;

        public void SetWaitSpinner2(bool visible)
           => WaitSpinner2.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;

        public void SetWaitSpinner1(bool visible)
           => WaitSpinner1.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        
        private ViewModel? _model;

        public ViewModel? Model
        {
            get { return _model; }
            set { DataContext = _model = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Model?.Dispose();
            Close();
        }        

        private async void Go_Click(object sender, RoutedEventArgs e)
        {
            if (CheckVarErsetzen.IsChecked == true && Model != null)
            {
                WaitSpinner3.Visibility = Visibility.Visible;

                if (Model.SelectedKalkulation == null)
                { TxtErsetzenOk.Text = "Die Kalkulation ist nicht vorhanden."; }

                await Task.Run(() =>  Model.ErsetzeVariable());

                TxtErsetzenOk.Text = "Es wurden keine Zeilen geändert.";
                TxtErsetzenOk.Foreground = Brushes.Red;

                if (Model?.SelectedProjekt == null || Model?.SelectedKalkulation == null)
                { return; }

                if (Model.ProtokollItems.Count > 0)
                {
                    TxtErsetzenOk.Text = $"Es wurden {Model.ProtokollItems.Count} Kalkulationszeilen geändert.";
                    TxtErsetzenOk.Foreground = Brushes.Green;
                    Model.ReloadKalkulation();
                }

                WaitSpinner3.Visibility = Visibility.Collapsed;

                string nrBezeichnung = $"{Model.SelectedProjekt.Nummer} - {Model.SelectedProjekt.Bezeichnung}";
                ProtokollLogger.WriterProtokoll(Model.ProtokollItems.OrderBy(_ => _.Nummer).ToList(), nrBezeichnung);
            }
        }

        private void Tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is LvPosition position)
            {                
                Model?.LoadKalkulationsbältter(position);
            }
        }
    }
}

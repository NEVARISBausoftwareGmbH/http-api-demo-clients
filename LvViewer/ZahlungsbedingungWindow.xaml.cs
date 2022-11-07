using Nevaris.Build.ClientApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lv_Viewer
{
    /// <summary>
    /// Interaction logic for ZahlungsbedingungWindow.xaml
    /// </summary>
    public partial class ZahlungsbedingungWindow : Window, INotifyPropertyChanged       
    {
        public ZahlungsbedingungWindow(Zahlungsbedingung zahlungsbedingung)
        {
            InitializeComponent();

            Load(zahlungsbedingung);
        }

        private void Load(Zahlungsbedingung zb)
        {            
            Einheit.Add(FälligkeitEinheit.Tage);
            Einheit.Add(FälligkeitEinheit.Werktage);
            Einheit.Add(FälligkeitEinheit.Wochen);
            Einheit.Add(FälligkeitEinheit.Monate);

            Zb = zb;
            if (Zb.Fälligkeit == null) { Zb.Fälligkeit = new() { Einheit = FälligkeitEinheit.Tage }; }
            if (Zb.Skonto1 == null)
            {
                Zb.Skonto1 = new()
                {
                    Fälligkeit = new()
                    {
                        Einheit = FälligkeitEinheit.Tage
                    }
                };
            }
            if (Zb.Skonto2 == null)
            {
                Zb.Skonto2 = new()
                {
                    Fälligkeit = new()
                    {
                        Einheit = FälligkeitEinheit.Tage
                    }
                };
            }
            if (Zb.Skonto3 == null)
            {
                Zb.Skonto3 = new()
                {
                    Fälligkeit = new()
                    {
                        Einheit = FälligkeitEinheit.Tage
                    }
                };
            }

            OnPropertyChanged(nameof(Zb));
        }

        public bool MustUpdate { get; set; }
        private Zahlungsbedingung _zb;

        public Zahlungsbedingung Zb
        {
            get { return _zb; }
            set { _zb = value; OnPropertyChanged(nameof(Zb)); }
        }

        private List<FälligkeitEinheit> _einheit = new();

        public List<FälligkeitEinheit> Einheit
        {
            get { return _einheit; }
            set 
            { 
                _einheit = value; 
                OnPropertyChanged(nameof(Einheit));                
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string property)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            MustUpdate = true;
            Close();
        }

        private void cc_Unloaded(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

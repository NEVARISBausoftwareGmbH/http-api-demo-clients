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

        public void SetWaitSpinner(bool visible)
           => WaitSpinner.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;

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

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            if (CheckVarErsetzen.IsChecked == true && Model != null)
            {
                WaitSpinner1.Visibility = Visibility.Visible;

                var result = Model.ErsetzeVariable();

                TxtErsetzenOk.Text = result.Message;
                TxtErsetzenOk.Foreground = Brushes.Red;

                if (Model?.SelectedProjekt == null || Model?.SelectedKalkulation == null)
                { return; }

                if (result.Success)
                {
                    TxtErsetzenOk.Foreground = Brushes.Green;
                    Model.ReloadKalkulation();
                }

                WaitSpinner1.Visibility = Visibility.Collapsed;
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

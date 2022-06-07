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

namespace HttpApi_Wpf_Bommhardt
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

        private ViewModel? _model;

        public ViewModel? Model
        {
            get { return _model; }
            set { DataContext = _model = value; }
        }

        public void SetTreeViewSource()
            => tv.ItemsSource = Model?.LvDetails?.RootNodes;

        public void SetWaitSpinner(bool visible)
            => WaitSpinner.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Model?.Dispose();
            Close();
        }

        private void tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //Langetext angezeigen
            if (e.NewValue != null && Model?.LvDetails != null)
            {
                Model.LvDetails.SelectedLvItem = e.NewValue as LvItem;
            }

        }
    }
}

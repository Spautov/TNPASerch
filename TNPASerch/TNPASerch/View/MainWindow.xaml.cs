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
using TNPASerch.View;
using TNPASerch.ViewModel;

namespace TNPASerch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void add_Tnpa(object sender, RoutedEventArgs e)
        {
            AddTNPAWindow addWindow = new AddTNPAWindow();
            addWindow.DataContext = new AddTNPAViewModel(addWindow);
            addWindow.Show();
        }
    }
}

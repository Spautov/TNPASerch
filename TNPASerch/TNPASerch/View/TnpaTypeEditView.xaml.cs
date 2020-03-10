using System.Windows;

namespace TNPASerch.View
{
    /// <summary>
    /// Interaction logic for TnpaTypeEditView.xaml
    /// </summary>
    public partial class TnpaTypeEditView : Window
    {
        public TnpaTypeEditView()
        {
            InitializeComponent();
        }

        private void CommandCloseBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}

using System.Windows;

namespace TNPASerch.View
{
    /// <summary>
    /// Interaction logic for AddTextView.xaml
    /// </summary>
    public partial class AddTextView : Window
    {
        public AddTextView()
        {
            InitializeComponent();
        }

        private void CommandOkBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }

        private void CommandCloseBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}

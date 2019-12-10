using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class AddTNPAViewModel
    {
        public AddTNPAViewModel()
        {
            NewWindow = new RelayCommand(NewWindows);
        }

        private void NewWindows()
        {
            var asdl = new AddTNPAWindow();
            asdl.Show();
        }

        public ICommand NewWindow { get; set; }
    }
}

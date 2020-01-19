using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class TnpaChengesEditViewModel : BaseViewModel
    {
        public ICommand AddChangeCommand { get; set; }
        public ICommand RemoveChangeCommand { get; set; }
        public ICommand EditChangeCommand { get; set; }
        public ICommand CancelChangeCommand { get; set; }


        public TnpaChengesEditViewModel(TnpaChengesEditView window) : base(window)
        {
                
        }
    }
}

using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class EditTNPAViewModel : BaseTNPAViewModel
    {
        public EditTNPAViewModel(TNPAWindow window, Tnpa tnpa) : base(window)
        {
            _currentTnpa = tnpa?? throw new ArgumentNullException(nameof(tnpa));

            GetTnpaTypsAsync();

            YearTnpa = _currentTnpa.Year.ToString();
            CountChanges = _currentTnpa.Changes.Count;

            PutIntoOperationTnpa = _currentTnpa.PutIntoOperation;
            CancelledTnpa = _currentTnpa.Cancelled;
            Registered = _currentTnpa.Registered;
        }

        protected override void Apply()
        {
            throw new NotImplementedException();
        }

        protected override void EditChanges()
        {
            throw new NotImplementedException();
        }

        protected override void Save()
        {
            throw new NotImplementedException();
        }
    }
}

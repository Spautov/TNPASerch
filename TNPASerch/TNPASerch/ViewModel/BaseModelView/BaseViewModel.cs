using System.Windows;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public abstract class BaseViewModel : NotifyPropertyChangedModel
    {
        protected bool YesNoCancelMessage(string message = "", string title = "")
        {
            MessageView messageView = ViewsManager.YesNoCancelMessage(message, title);
            messageView.ShowDialog();
            return messageView.DialogResult.GetValueOrDefault();
        }

        protected bool YesCancelMessage(string message = "", string title = "")
        {
            MessageView messageView = ViewsManager.YesCancelMessage(message, title);
            messageView.ShowDialog();
            return messageView.DialogResult.GetValueOrDefault();
        }

        protected bool YesMessage(string message = "", string title = "")
        {
            MessageView messageView = ViewsManager.YesMessage(message, title);
            messageView.ShowDialog();
            return messageView.DialogResult.GetValueOrDefault();
        }
    }
}

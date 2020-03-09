using DbWorker;
using Microsoft.EntityFrameworkCore;
using Ninject;
using Ninject.Modules;
using System.Windows;

namespace TNPASerch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel container;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();

        }

        private void ConfigureContainer()
        {
            this.container = new StandardKernel();
        }

        private void ComposeObjects()
        {
            Current.MainWindow = this.container.Get<MainWindow>();
        }

    }
}

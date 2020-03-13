using DbWorker;
using Microsoft.EntityFrameworkCore;
using Ninject;
using Ninject.Modules;
using Repositories;
using System.Windows;

namespace TNPASerch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IKernel Container { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            Container = new StandardKernel();
        }

        private void ComposeObjects()
        {
            Current.MainWindow = Container.Get<MainWindow>();
            Container.Bind<IFileRepository>().To<FileRepository>().InSingletonScope()
               .WithConstructorArgument("directoryName", "Data");
        }

    }
}

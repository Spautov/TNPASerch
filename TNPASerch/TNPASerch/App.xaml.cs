using DataLoader;
using DataServices;
using DbWorker;
using Microsoft.EntityFrameworkCore;
using Ninject;
using Ninject.Modules;
using Repositories;
using Searcher;
using System.Threading.Tasks;
using System.Windows;
using TextDocumentReaders;

namespace TNPASerch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string PDFNamed = "PDF";
        private const string WordNamed = "Word";
        private const string TxtNamed = "Txt";
        
        public static IKernel Container { get; private set; }
        public static WebDataLoader dataLoader { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
            dataLoader = Container.Get<WebDataLoader>();
            _ = LoadData();
        }

        private void ConfigureContainer()
        {
            Container = new StandardKernel();
        }

        private void ComposeObjects()
        {
            Container.Bind<IRepository>().To<SQLiteRepository>().InSingletonScope();
            Container.Bind<IFileRepository>().To<FileRepository>().InSingletonScope()
               .WithConstructorArgument("directoryName", "Data");
            Container.Bind<ITextDocumentReader>().To<PDFDocumentReader>().Named(PDFNamed);
            Container.Bind<ITextDocumentReader>().To<WordDocumentReader>().Named(WordNamed);
            Container.Bind<ITextDocumentReader>().To<TxtDocumentReader>().Named(TxtNamed);
            Container.Bind<ISearcher>().To<LuceneSercher>().InSingletonScope()
                .WithConstructorArgument("directoryName", "IndexData")
                .WithConstructorArgument("pdfReader", Container.Get<ITextDocumentReader>(PDFNamed))
                .WithConstructorArgument("wordReader", Container.Get<ITextDocumentReader>(WordNamed))
                .WithConstructorArgument("txtReader", Container.Get<ITextDocumentReader>(TxtNamed));
            Container.Bind<WebDataLoader>().To<WebDataLoader>().InSingletonScope();
            Container.Bind<DataService>().To<DataService>().InSingletonScope();

            Current.MainWindow = Container.Get<MainWindow>();
        }

        private async Task LoadData()
        {
            var res = await dataLoader.GetDataAsync();
        }
    }
}

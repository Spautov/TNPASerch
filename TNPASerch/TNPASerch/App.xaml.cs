using DbWorker;
using Microsoft.EntityFrameworkCore;
using Ninject;
using Ninject.Modules;
using Repositories;
using Searcher;
using System.Windows;
using TextDocumentReaders;

namespace TNPASerch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly string PDFNamed = "PDF";
        private readonly string WordNamed = "Word";
        private readonly string TxtNamed = "Txt";
        
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
            Container.Bind<IRepository>().To<SQLiteRepository>().InSingletonScope();
            Container.Bind<IFileRepository>().To<FileRepository>().InSingletonScope()
               .WithConstructorArgument("directoryName", "Data");
            Container.Bind<ITextDocumentReader>().To<PDFDocumentReader>().Named(PDFNamed);
            Container.Bind<ITextDocumentReader>().To<WordDocumentReader>().Named(WordNamed);
            Container.Bind<ITextDocumentReader>().To<TxtDocumentReader>().Named(TxtNamed);
            Container.Bind<ISearcher>().To<LuceneSercher>()
                .WithConstructorArgument("directoryName", "IndexData")
                .WithConstructorArgument("pdfReader", Container.Get<ITextDocumentReader>(PDFNamed))
                .WithConstructorArgument("wordReader", Container.Get<ITextDocumentReader>(WordNamed))
                .WithConstructorArgument("txtReader", Container.Get<ITextDocumentReader>(TxtNamed));

            Current.MainWindow = Container.Get<MainWindow>();
        }
    }
}

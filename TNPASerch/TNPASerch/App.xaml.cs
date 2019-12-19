using DbWorker;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace TNPASerch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var db = new TnpaDbContext())
            {
                db.Database.Migrate();
            }
        }

    }
}

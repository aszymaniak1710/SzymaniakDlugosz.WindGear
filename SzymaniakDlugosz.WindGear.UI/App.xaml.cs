using System.Windows;
using SzymaniakDlugosz.WindGear.BL;
using SzymaniakDlugosz.WindGear.Interfaces;

namespace SzymaniakDlugosz.WindGear.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // Dependency Injection
                IBLService blService = new BLService();
                MainViewModel mainViewModel = new MainViewModel(blService);
                
                MainWindow window = new MainWindow();
                window.DataContext = mainViewModel;
                window.Show();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Startup Error: {ex.Message}\nCheck App.config for DAOLibrary settings.", "WindGear Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }
    }
}

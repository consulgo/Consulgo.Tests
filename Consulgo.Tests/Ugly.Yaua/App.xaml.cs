using System.Windows;

namespace Ugly.Yaua
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0 && e.Args[0].Equals("--TryError", System.StringComparison.InvariantCulture))
            {
                MessageBox.Show("This is test error message. Application will abort",
                    "Error during start", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(2);
            }
            else
            {
                SplashScreen s = new SplashScreen();
                s.Show();
            }
        }
    }
}

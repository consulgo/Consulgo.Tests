using System.Windows;
using System.Windows.Automation;
using Ugly.Yaua.AutomationIds;
using Ugly.Yaua.ViewModels;

namespace Ugly.Yaua
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DirtyWorkaroundToLoadAutomationIdsDll();
            DataContext = new MainViewModel();
        }

        private void DirtyWorkaroundToLoadAutomationIdsDll()
        {
            SetValue(AutomationProperties.AutomationIdProperty, MainWindowIds.MainWindow);
        }
    }
}

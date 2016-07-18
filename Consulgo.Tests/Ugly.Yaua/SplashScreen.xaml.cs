using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Threading;
using System.Windows;

namespace Ugly.Yaua
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        private IDisposable _disposable;
        private CancellationDisposable _cancellationDisposable;

        public SplashScreen()
        {
            InitializeComponent();

            Application.Current.Exit += Current_Exit;
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            if (_cancellationDisposable != null)
            {
                _cancellationDisposable.Dispose();
            }

            if (_disposable != null)
            {
                _disposable.Dispose();
            }
        }

        /// <summary>
        /// Some code to have random start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _disposable = NewThreadScheduler.Default.Schedule(() =>
            {
                _cancellationDisposable = new CancellationDisposable();
                var r = new Random(DateTime.Now.Millisecond);
                var seconds = r.Next(20);
                Dispatcher.BeginInvoke((Action)delegate
                {
                    Counter.Content = string.Format("{0} second(s)", seconds);
                });

                for (int i = 0; i < seconds; i++)
                {
                    if (!_cancellationDisposable.Token.IsCancellationRequested)
                    {
                        Thread.Sleep(1000);
                    }
                }

                Dispatcher.BeginInvoke((Action)delegate
                {
                    var mainWindow = new MainWindow();
                    Close();
                    mainWindow.Show();
                });
            });
        }
    }
}

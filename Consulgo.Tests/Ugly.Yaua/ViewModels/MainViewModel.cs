
namespace Ugly.Yaua.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public HelloWorldViewModel HelloWorld { get; set; }

        public MainViewModel()
        {
            HelloWorld = new HelloWorldViewModel();
        }
    }
}

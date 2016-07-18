using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ugly.Yaua.ViewModels
{
    public class HelloWorldViewModel : BaseViewModel
    {
        public ICommand SayHello { get; private set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaiseProperty("Name");
            }
        }

        private string _result;

        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                RaiseProperty("Result");
            }
        }

        public HelloWorldViewModel()
        {
            SayHello = new SimpleCommand(() => Result = string.Format("Hello {0}!", Name));
        }
    }
}

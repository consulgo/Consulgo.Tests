using Consulgo.Test.ReactiveAutomation.Controls;
using Ugly.Yaua.AutomationIds;

namespace Consulgo.Test.ReactiveAutomation.SampleRunner.UiAutomation.Models
{
    public class MainWindowModel : WindowControl
    {
        public HelloWorldTabModel HelloWorldTab { get; set; }

        public MainWindowModel()
        {
            HelloWorldTab = AutomationControlDescription.ByName(MainWindowIds.HelloWorldTabHeader)
                            .AsDescendantOf<HelloWorldTabModel>(this);
        }
    }
}

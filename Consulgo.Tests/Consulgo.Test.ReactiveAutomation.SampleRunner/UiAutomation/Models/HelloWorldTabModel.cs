using Consulgo.Test.ReactiveAutomation.Controls;
using Ugly.Yaua.AutomationIds;

namespace Consulgo.Test.ReactiveAutomation.SampleRunner.UiAutomation.Models
{
    public class HelloWorldTabModel : TabItemControl
    {
        public HelloWorldModel HelloWorld { get; set; }

        public HelloWorldTabModel()
        {
            HelloWorld = AutomationControlDescription.ById(MainWindowIds.HelloWorldPane)
                            .ChildOf(this)
                            .As<HelloWorldModel>();
        }
    }
}

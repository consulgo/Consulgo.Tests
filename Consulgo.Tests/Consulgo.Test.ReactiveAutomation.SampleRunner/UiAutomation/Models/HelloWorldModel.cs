using Consulgo.Test.ReactiveAutomation.Controls;
using Ugly.Yaua.AutomationIds;

namespace Consulgo.Test.ReactiveAutomation.SampleRunner.UiAutomation.Models
{
    public class HelloWorldModel : PaneControl
    {
        public TextControl Name { get; set; }
        public ButtonControl AcceptButton { get; set; }
        public TextControl Result { get; set; }

        public HelloWorldModel()
        {
            Name = AutomationControlDescription.ById(MainWindowIds.HelloWorldName)
                            .ChildOf(this)
                            .As<TextControl>();

            AcceptButton = AutomationControlDescription.ById(MainWindowIds.HelloWorldButton)
                            .ChildOf(this)
                            .As<ButtonControl>();

            Result = AutomationControlDescription.ById(MainWindowIds.HelloWorldResult)
                            .ChildOf(this)
                            .As<TextControl>();
        }
    }
}

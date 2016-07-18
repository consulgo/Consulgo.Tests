using System.Windows.Automation;

namespace Consulgo.Test.ReactiveAutomation
{
    /// <summary>
    /// Base Automation Message
    /// </summary>
    public class AutomationMessage
    {
        public AutomationElement SourceElement { get; set; }

        public AutomationMessage()
        {
        }

        public AutomationMessage(AutomationElement sourceElement)
        {
            SourceElement = sourceElement;
        }
    }
}

using System.Windows.Automation;

namespace Consulgo.Test.ReactiveAutomation
{
    /// <summary>
    /// Focus changes stream message
    /// </summary>
    public class FocusChangeMessage : AutomationMessage
    {
        public AutomationFocusChangedEventArgs Args { get; set; }

        public FocusChangeMessage(AutomationElement sourceElement, AutomationFocusChangedEventArgs args)
            : base(sourceElement)
        {
            Args = args;
        }
    }
}

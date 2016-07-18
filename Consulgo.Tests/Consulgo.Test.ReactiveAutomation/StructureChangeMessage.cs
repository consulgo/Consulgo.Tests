using System.Windows.Automation;

namespace Consulgo.Test.ReactiveAutomation
{
    /// <summary>
    /// Strcuture change stream message
    /// </summary>
    public class StructureChangeMessage : AutomationMessage
    {
        public StructureChangedEventArgs Args { get; set; }

        public StructureChangeMessage(AutomationElement sourceElement, StructureChangedEventArgs args)
            : base(sourceElement)
        {
            Args = args;
        }
    }
}

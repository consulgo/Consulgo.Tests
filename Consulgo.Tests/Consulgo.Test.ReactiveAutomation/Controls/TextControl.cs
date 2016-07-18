
namespace Consulgo.Test.ReactiveAutomation.Controls
{
    /// <summary>
    /// Text Automation Control
    /// </summary>
    public class TextControl : AutomationControl
    {
        public string Value
        {
            get { return InternalValue; }
            set { InternalValue = value; }
        }
    }
}

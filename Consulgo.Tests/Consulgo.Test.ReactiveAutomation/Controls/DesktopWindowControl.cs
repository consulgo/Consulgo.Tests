using System.Windows.Automation;

namespace Consulgo.Test.ReactiveAutomation.Controls
{
    /// <summary>
    /// Desktop Window Automation Control
    /// </summary>
    public class DesktopWindowControl : WindowControl
    {
        public DesktopWindowControl()
        {
            var c = AutomationElement.RootElement;
            AutomationElement = c;
            Description = AutomationControlDescription.ByNativeWindowHandle(c.Current.NativeWindowHandle).ChildOf(this);
        }

        public static DesktopWindowControl NewInstance
        {
            get
            {
                return new DesktopWindowControl();
            }
        }
    }
}

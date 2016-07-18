using System.Windows.Automation;

namespace Consulgo.Test.ReactiveAutomation.Controls
{
    /// <summary>
    /// Simple description for automation control
    /// </summary>
    public class AutomationControlDescription
    {
        /// <summary>
        /// Factory method to construct description based on <see cref="AutomationElement.NativeWindowHandleProperty"/>
        /// </summary>
        /// <param name="handle">Native handle of automation element</param>
        /// <returns></returns>
        public static AutomationControlDescription ByNativeWindowHandle(int handle)
        {
            return new AutomationControlDescription(AutomationElement.NativeWindowHandleProperty, handle);
        }


        /// <summary>
        /// Factory method to construct description based on <see cref="AutomationElement.NameProperty"/>
        /// </summary>
        /// <param name="name">Name of automation element</param>
        /// <returns></returns>
        public static AutomationControlDescription ByName(string name)
        {
            name.ThrowIfNullOrEmptyArg("name");
            return new AutomationControlDescription(AutomationElement.NameProperty, name);
        }

        /// <summary>
        /// Factory method to construct description based on <see cref="AutomationElement.AutomationIdProperty"/>
        /// </summary>
        /// <param name="automationId">Automation Identifier of automation element</param>
        /// <returns></returns>
        public static AutomationControlDescription ById(string automationId)
        {
            automationId.ThrowIfNullOrEmptyArg("automationId");
            return new AutomationControlDescription(AutomationElement.AutomationIdProperty, automationId);
        }

        protected readonly PropertyCondition _propertyCondition;
        public AutomationControl ParentControl { get; set; }
        public TreeScope Scope { get; set; }

        protected AutomationControlDescription(PropertyCondition propertyCondition)
        {
            propertyCondition.ThrowIfNullArg("propertyCondition");
            _propertyCondition = propertyCondition;
        }

        protected AutomationControlDescription(AutomationProperty property, string value)
            : this(new PropertyCondition(property, value))
        {
        }

        protected AutomationControlDescription(AutomationProperty property, int value)
            : this(new PropertyCondition(property, value))
        {
        }

        public PropertyCondition PropertyCondition
        {
            get
            {
                _propertyCondition.ThrowIfNull("PropertyCondition must be set first");
                return _propertyCondition;
            }
        }
    }
}

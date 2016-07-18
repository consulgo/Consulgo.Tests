using System;
using System.Reactive;
using System.Windows.Automation;

namespace Consulgo.Test.ReactiveAutomation.Controls
{
    public class AutomationControl
    {
        private AutomationElement _automationElement;
        private AutomationControlDescription _desc;

        public AutomationControlDescription Description
        {
            get
            {
                _desc.ThrowIfNull("Description must be set before use");
                return _desc;
            }
            set { _desc = value; }
        }

        public AutomationElement AutomationElement
        {
            get { return _automationElement; }
            set { _automationElement = value; }
        }

        protected AutomationElement InternalAutomationElement
        {
            get
            {
                _desc.ThrowIfNull("AutomationElement must be set before use");
                return _automationElement;
            }
        }

        public IObservable<Unit> WaitForElement(bool closeAfter = true)
        {
            return this.WaitForIt(closeAfter);
        }

        public string Name
        {
            get
            {
                return InternalAutomationElement.Current.Name;
            }
        }

        protected string InternalValue
        {
            get
            {
                return ValuePattern.Current.Value;
            }
            set
            {
                ValuePattern.SetValue(value);
            }
        }

        protected void InternalInvoke()
        {
            InvokePattern.Invoke();
        }

        protected void InternalSelectItem()
        {
            SelectionItemPattern.Select();
        }

        protected ValuePattern ValuePattern
        {
            get
            {
                return GetPattern<ValuePattern>(ValuePattern.Pattern);
            }
        }

        protected InvokePattern InvokePattern
        {
            get
            {
                return GetPattern<InvokePattern>(InvokePattern.Pattern);
            }
        }

        protected SelectionItemPattern SelectionItemPattern
        {
            get
            {
                return GetPattern<SelectionItemPattern>(SelectionItemPattern.Pattern);
            }
        }

        protected SelectionPattern SelectionPattern
        {
            get
            {
                return GetPattern<SelectionPattern>(SelectionPattern.Pattern);
            }
        }

        protected TPattern GetPattern<TPattern>(AutomationPattern pattern)
            where TPattern : BasePattern
        {
            return InternalAutomationElement.GetCurrentPattern(pattern) as TPattern;
        }
    }
}

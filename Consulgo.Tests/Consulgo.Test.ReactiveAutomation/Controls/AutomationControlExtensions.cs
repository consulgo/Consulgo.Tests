using System.Windows.Automation;

namespace Consulgo.Test.ReactiveAutomation.Controls
{
    public static class AutomationControlExtensions
    {
        /// <summary>
        /// Sets given control as child of parent control
        /// </summary>
        /// <param name="desc">Input description</param>
        /// <param name="parentControl">Parent control reference</param>
        /// <returns>Input description</returns>
        public static AutomationControlDescription ChildOf(this AutomationControlDescription desc, AutomationControl parentControl)
        {
            SetupValues(desc, parentControl, TreeScope.Children);
            return desc;
        }

        /// <summary>
        /// Sets given control as descendant of parent control
        /// </summary>
        /// <param name="desc">Input description</param>
        /// <param name="parentControl">Parent control reference</param>
        /// <returns>Input description</returns>
        public static AutomationControlDescription AsDescendant(this AutomationControlDescription desc, AutomationControl parentControl)
        {
            SetupValues(desc, parentControl, TreeScope.Descendants);
            return desc;
        }


        /// <summary>
        /// Sets given control as descendant of parent control and creates an instance
        /// </summary>
        /// <typeparam name="T">Type of control</typeparam>
        /// <param name="desc">Input description</param>
        /// <param name="parentControl">Parent control reference</param>
        /// <returns>Input description</returns>
        public static T AsChildOf<T>(this AutomationControlDescription desc, AutomationControl parentControl)
            where T : AutomationControl, new()
        {
            SetupValues(desc, parentControl, TreeScope.Children);
            var t = new T();
            t.Description = desc;
            return t;
        }

        /// <summary>
        /// Sets given control as child of parent control and creates an instance
        /// </summary>
        /// <typeparam name="T">Type of control</typeparam>
        /// <param name="desc">Input description</param>
        /// <param name="parentControl">Parent control reference</param>
        /// <returns>Input description</returns>
        public static T AsDescendantOf<T>(this AutomationControlDescription desc, AutomationControl parentControl)
            where T : AutomationControl, new()
        {
            SetupValues(desc, parentControl, TreeScope.Descendants);
            var t = new T();
            t.Description = desc;
            return t;
        }

        /// <summary>
        /// creates new instance of control based on given description
        /// </summary>
        /// <typeparam name="T">Type of control</typeparam>
        /// <param name="desc">Control description</param>
        /// <returns>Control instance</returns>
        public static T As<T>(this AutomationControlDescription desc)
            where T : AutomationControl, new()
        {
            var t = new T();
            t.Description = desc;
            return t;
        }

        private static void SetupValues(AutomationControlDescription desc, AutomationControl parentControl, TreeScope scope)
        {
            desc.ParentControl = parentControl;
            desc.Scope = scope;
        }
    }
}

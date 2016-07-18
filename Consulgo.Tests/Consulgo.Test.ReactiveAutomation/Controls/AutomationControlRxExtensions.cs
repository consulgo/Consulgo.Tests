using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Automation;

namespace Consulgo.Test.ReactiveAutomation.Controls
{
    public static class AutomationControlRxExtensions
    {
        /// <summary>
        /// Streams updates of existance of the control
        /// </summary>
        /// <param name="control">Control to wait for existance</param>
        /// <param name="closeAfter">Should stream be closed after</param>
        /// <returns>Event updates stream</returns>
        public static IObservable<Unit> WaitForIt(this AutomationControl control, bool closeAfter = true)
        {
            if (control.AutomationElement != null)
            {
                return Observable.Create<Unit>(o =>
                    {
                        o.OnNext(Unit.Default);

                        if (closeAfter)
                        {
                            o.OnCompleted();
                        }

                        return Disposable.Empty;
                    });
            }

            return Chain<Unit>(
                control.Description.ParentControl.WaitForIt(true),
                () => control
                            .Description
                            .ParentControl
                            .AutomationElement
                            .WaitForFirstElement(
                                control.Description.PropertyCondition,
                                control.Description.Scope,
                                closeAfter)
                            .Select(PersistAutomationElement(control))
                );
        }

        private static Func<AutomationElement, Unit> PersistAutomationElement(AutomationControl control)
        {
            return element =>
            {
                control.AutomationElement = element;
                return Unit.Default;
            };
        }

        private static IObservable<T> Chain<T>(this IObservable<T> previous, Func<IObservable<T>> nextProducer)
        {
            return previous.IsEmpty().SelectMany(Observable.Defer(nextProducer));
        }
    }
}

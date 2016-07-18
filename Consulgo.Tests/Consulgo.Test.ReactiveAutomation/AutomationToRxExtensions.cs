using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Automation;

namespace Consulgo.Test.ReactiveAutomation
{
    public static class AutomationToRxExtensions
    {
        /// <summary>
        /// Observable for changes of element properties
        /// </summary>
        /// <param name="element">Element to observe</param>
        /// <param name="properties">Properties to observe</param>
        /// <param name="scope">Scope of changes</param>
        /// <returns></returns>
        public static IObservable<Unit> CreatePropertyChangeStream(this AutomationElement element, AutomationProperty[] properties, TreeScope scope = TreeScope.Element)
        {
            return Observable.Create<Unit>(
                o =>
                {
                    var handler = new AutomationPropertyChangedEventHandler((sender, args) =>
                        {
                            o.OnNext(Unit.Default);
                        });
                    try
                    {
                        Automation.AddAutomationPropertyChangedEventHandler(element, scope, handler, properties);
                    }
                    catch (Exception ex)
                    {
                        o.OnError(ex);
                    }

                    return Disposable.Create(() => Automation.RemoveAutomationPropertyChangedEventHandler(element, handler));
                });
        }

        /// <summary>
        /// Observable for changes of element structure
        /// </summary>
        /// <param name="element">Element to observe</param>
        /// <param name="scope">Scope of changes</param>
        /// <returns></returns>
        public static IObservable<StructureChangeMessage> CreateStructureChangeStream(this AutomationElement element, TreeScope scope = TreeScope.Descendants)
        {
            return Observable.Create<StructureChangeMessage>(
                    o =>
                    {
                        var handler = new StructureChangedEventHandler((sender, args) =>
                                o.OnNext(new StructureChangeMessage(sender as AutomationElement, args)));

                        try
                        {
                            Automation.AddStructureChangedEventHandler(element, scope, handler);
                        }
                        catch (Exception ex)
                        {
                            o.OnError(ex);
                        }

                        return Disposable.Create(() => Automation.RemoveStructureChangedEventHandler(element, handler));
                    });
        }

        /// <summary>
        /// Observable for changes of focus
        /// </summary>
        /// <returns></returns>
        public static IObservable<FocusChangeMessage> CreateFocusChangeStream()
        {
            return Observable.Create<FocusChangeMessage>(
                    o =>
                    {
                        var handler = new AutomationFocusChangedEventHandler((sender, args) => o.OnNext(new FocusChangeMessage(sender as AutomationElement, args)));

                        try
                        {
                            Automation.AddAutomationFocusChangedEventHandler(handler);
                        }
                        catch (Exception ex)
                        {
                            o.OnError(ex);
                        }

                        return Disposable.Create(() => Automation.RemoveAutomationFocusChangedEventHandler(handler));
                    });
        }
    }
}

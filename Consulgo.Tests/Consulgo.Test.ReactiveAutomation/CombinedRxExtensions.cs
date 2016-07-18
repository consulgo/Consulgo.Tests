using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Automation;

namespace Consulgo.Test.ReactiveAutomation
{
    public static class CombinedRxExtensions
    {
        /// <summary>
        /// Observable for changes of properties in given element
        /// </summary>
        /// <param name="element">Element to observe changes</param>
        /// <param name="properties">Properties to observe</param>
        /// <param name="scope">Scope of changes</param>
        /// <param name="closeAfter">Flag determines if output stream will be completed after change</param>
        /// <returns></returns>
        public static IObservable<Unit> WaitForChange(
            this AutomationElement element,
            AutomationProperty[] properties,
            TreeScope scope = TreeScope.Element,
            bool closeAfter = true)
        {
            element.ThrowIfNullArg("element", "Element cannot be null");
            properties.ThrowIfNullArg("properties", "Properties cannot be null");
            properties.ThrowIfEmpty("properties", "Properties cannot be empty");

            return Observable.Create<Unit>(o =>
                {
                    var propertyDisposable = element.CreatePropertyChangeStream(properties, scope)
                        .Subscribe(u =>
                        {
                            o.OnNext(u);

                            if(closeAfter)
                            {
                                o.OnCompleted();
                            }
                        },
                        o.OnError,
                        o.OnCompleted);
                    return Disposable.Create(propertyDisposable.Dispose);
                });
        }

        /// <summary>
        /// Observable for changes of structre in given element
        /// </summary>
        /// <param name="parent">Parent element to observe structure</param>
        /// <param name="condition">Condition used to limit observed changes</param>
        /// <param name="scope">Scope of changes</param>
        /// <param name="closeAfter">Flag determines if output stream will be completed after change</param>
        /// <returns>Stream of observed new Controls</returns>
        public static IObservable<AutomationElement> WaitForFirstElement(
            this AutomationElement parent,
            Condition condition,
            TreeScope scope = TreeScope.Descendants,
            bool closeAfter = true)
        {
            parent.ThrowIfNullArg("parent", "Parent element cannot be null");
            condition.ThrowIfNullArg("condition", "Condition cannot be null");

            return Observable.Create<AutomationElement>(o =>
            {
                var structureDisposable = parent.CreateStructureChangeStream(scope)
                    .Where(msg => VerifyChangesInStructure(msg))
                    .Subscribe(_ => FindElementAndPush(parent, condition, scope, o, closeAfter));
                FindElementAndPush(parent, condition, scope, o, closeAfter);
                return Disposable.Create(structureDisposable.Dispose);
            });
        }

        private static bool VerifyChangesInStructure(StructureChangeMessage msg)
        {
            return (msg.Args.StructureChangeType == StructureChangeType.ChildAdded
                    || msg.Args.StructureChangeType == StructureChangeType.ChildrenBulkAdded
                    || msg.Args.StructureChangeType == StructureChangeType.ChildrenInvalidated);
        }

        private static bool FindElementAndPush(AutomationElement parent, Condition condition, TreeScope scope, IObserver<AutomationElement> result, bool completeAfter)
        {
            var element = parent.FindFirst(scope, condition);

            if (element != null)
            {
                result.OnNext(element);

                if (completeAfter)
                {
                    result.OnCompleted();
                }

                return true;
            }

            return false;
        }
    }
}

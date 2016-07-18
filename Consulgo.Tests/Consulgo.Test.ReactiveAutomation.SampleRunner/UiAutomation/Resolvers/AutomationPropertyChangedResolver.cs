using Consulgo.Test.ReactiveAutomation.Controls;
using Consulgo.Test.ReactiveBdd;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Automation;

namespace Consulgo.Test.ReactiveAutomation.SampleRunner.UiAutomation.Resolvers
{
    /// <summary>
    /// Resolves element that is to be shown on the screen and its properties setup
    /// </summary>
    public class AutomationPropertyChangedResolver : BaseAutomationResolver
    {
        private readonly AutomationProperty[] _properties;

        /// <summary>
        /// Constructor for resolver
        /// </summary>
        /// <param name="properties">Properties to be setup on the component</param>
        public AutomationPropertyChangedResolver(params AutomationProperty[] properties)
        {
            _properties = properties;
        }

        protected override IObservable<ApplicationState<UglyApplicationModel>> ResolveImpl<T>(ReactiveBdd.ApplicationState<UglyApplicationModel> state, System.Linq.Expressions.Expression<Func<UglyApplicationModel, T>> propertyExpression)
        {
            var elementResolver = new AutomationPropertyResolver();
            return elementResolver.Resolve(state, propertyExpression)
                .Chain(() =>
                    Observable.Create<ApplicationState<UglyApplicationModel>>(o =>
                    {
                        AutomationControl tc = null;

                        try
                        {
                            var fun = propertyExpression.Compile();
                            tc = fun(state.Model) as AutomationControl;
                            return tc
                                .AutomationElement
                                .WaitForChange(_properties, TreeScope.Element, true)
                                .Subscribe(_ =>
                                    {
                                        o.OnNext(state);
                                    },
                                    o.OnError,
                                    o.OnCompleted);
                        }
                        catch (Exception ex)
                        {
                            o.OnError(ex);
                            return Disposable.Empty;
                        }
                    })
                );
        }
    }
}

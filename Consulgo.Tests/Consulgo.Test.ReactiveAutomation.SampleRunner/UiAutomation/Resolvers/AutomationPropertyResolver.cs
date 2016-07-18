using Consulgo.Test.ReactiveAutomation.Controls;
using Consulgo.Test.ReactiveBdd;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Consulgo.Test.ReactiveAutomation.SampleRunner.UiAutomation.Resolvers
{
    /// <summary>
    /// Resolves element that is to be shown on the screen
    /// </summary>
    public class AutomationPropertyResolver : BaseAutomationResolver
    {
        protected override IObservable<ApplicationState<UglyApplicationModel>> ResolveImpl<T>(ApplicationState<UglyApplicationModel> state, System.Linq.Expressions.Expression<Func<UglyApplicationModel, T>> propertyExpression)
        {
            return Observable.Create<ApplicationState<UglyApplicationModel>>(
                o =>
                {
                    AutomationControl tc = null;

                    try
                    {
                        var fun = propertyExpression.Compile();
                        tc = fun(state.Model) as AutomationControl;
                        return tc.WaitForIt(true).Subscribe(_ =>
                            {
                                o.OnNext(state);
                            }, o.OnError, o.OnCompleted);
                    }
                    catch(Exception ex)
                    {
                        o.OnError(ex);
                        return Disposable.Empty;
                    }
                });
        }
    }
}

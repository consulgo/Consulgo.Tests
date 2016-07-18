using Consulgo.Test.ReactiveBdd;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Consulgo.Test.ReactiveAutomation.SampleRunner.UiAutomation.Resolvers
{
    /// <summary>
    /// Base automation resolver. Used only for validation of parameters
    /// </summary>
    public abstract class BaseAutomationResolver : IPropertyResolver<UglyApplicationModel>
    {
        public IObservable<ApplicationState<UglyApplicationModel>> Resolve<T>(ApplicationState<UglyApplicationModel> state, System.Linq.Expressions.Expression<Func<UglyApplicationModel, T>> propertyExpression)
        {
            ValidateExpression<T>(propertyExpression);
            return ResolveImpl(state, propertyExpression);
        }

        protected abstract IObservable<ApplicationState<UglyApplicationModel>> ResolveImpl<T>(ApplicationState<UglyApplicationModel> state, System.Linq.Expressions.Expression<Func<UglyApplicationModel, T>> propertyExpression);

        private static void ValidateExpression<T>(System.Linq.Expressions.Expression<Func<UglyApplicationModel, T>> propertyExpression)
        {
            if (propertyExpression.NodeType != ExpressionType.Lambda)
            {
                throw new NotSupportedException("Use only simple lambdas");
            }

            if (propertyExpression.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new NotSupportedException("Use only members");
            }

            var me = propertyExpression.Body as MemberExpression;

            if (me == null)
            {
                throw new NotSupportedException("Use only member expressions");
            }

            var pi = me.Member as PropertyInfo;

            if (pi == null)
            {
                throw new NotSupportedException("Use only public properties as members");
            }

            var act = typeof(ReactiveAutomation.Controls.AutomationControl);

            if (!act.IsAssignableFrom(pi.PropertyType))
            {
                throw new NotSupportedException("Use only types assignable from AutomationControl");
            }
        }
    }
}

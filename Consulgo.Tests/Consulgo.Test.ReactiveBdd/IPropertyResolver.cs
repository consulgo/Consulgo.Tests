using System;
using System.Linq.Expressions;

namespace Consulgo.Test.ReactiveBdd
{
    /// <summary>
    /// Evaluates expression with given application state
    /// </summary>
    /// <typeparam name="TModel">Model of the system under test</typeparam>
    public interface IPropertyResolver<TModel>
        where TModel : IModel
    {
        /// <summary>
        /// Resolves expression in <see cref="propertyExpression"/>
        /// on given <see cref="state"/>
        /// </summary>
        /// <typeparam name="T">Model of the System Under Test</typeparam>
        /// <param name="state">Actual state of model</param>
        /// <param name="propertyExpression">Expression to be evaluated</param>
        /// <returns>Result stream</returns>
        IObservable<ApplicationState<TModel>> Resolve<T>(
            ApplicationState<TModel> state,
            Expression<Func<TModel, T>> propertyExpression);
    }
}

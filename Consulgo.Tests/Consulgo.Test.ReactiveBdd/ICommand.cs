using System;
using System.Reactive;

namespace Consulgo.Test.ReactiveBdd
{
    /// <summary>
    /// Command that implements a step in test
    /// </summary>
    /// <typeparam name="TModel">Model of the system under test</typeparam>
    public interface ICommand<TModel> where TModel : IModel
    {
        /// <summary>
        /// Executed when control is passed to the component.
        /// Output stream to be closed after execution
        /// </summary>
        /// <param name="model">Application model</param>
        /// <returns>Observable of changed state</returns>
        IObservable<Unit> Execute(ApplicationState<TModel> model);
    }
}

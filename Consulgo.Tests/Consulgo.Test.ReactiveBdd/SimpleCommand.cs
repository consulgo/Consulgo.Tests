using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Consulgo.Test.ReactiveBdd
{
    /// <summary>
    /// Simple evaluation of the statement
    /// </summary>
    /// <typeparam name="TModel">Model of the system under test</typeparam>
    public class SimpleCommand<TModel> : ICommand<TModel> where TModel : IModel
    {
        public Action<ApplicationState<TModel>> _action;

        /// <summary>
        /// Constructs the object
        /// </summary>
        /// <param name="applicationModel">Application state used in evaluaion</param>
        /// <param name="action">Action to be evaluated as a result</param>
        public SimpleCommand(Action action)
        {
            _action = (args) => action();
        }

        /// <summary>
        /// Constructs the object
        /// </summary>
        /// <param name="applicationModel">Application state used in evaluaion</param>
        /// <param name="action">Action to be evaluated as a result</param>
        public SimpleCommand(Action<ApplicationState<TModel>> action)
        {
            _action = action;
        }

        /// <summary>
        /// Does the evaluation of the given action
        /// </summary>
        /// <param name="args">Reserved</param>
        /// <returns>Result stream</returns>
        public virtual IObservable<Unit> Execute(ApplicationState<TModel> model)
        {
            return Observable.Create<Unit>(o =>
            {
                try
                {
                    _action(model);
                    o.OnNext(Unit.Default);
                    o.OnCompleted();
                }
                catch (Exception ex)
                {
                    o.OnError(ex);
                }

                return Disposable.Empty;
            });
        }
    }
}

using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Consulgo.Test.ReactiveBdd
{
    /// <summary>
    /// Command describing action and pre- and post-requisites
    /// </summary>
    /// <typeparam name="TModel">Model of the system under test</typeparam>
    public class RequireCommand<TModel> : SimpleCommand<TModel> where TModel : IModel
    {
        private readonly Func<Require<TModel>, Require<TModel>> _requireFunction;

        /// <summary>
        /// Construct the object
        /// </summary>
        /// <param name="applicationModel">Application state used in evaluaion</param>
        /// <param name="require">Producer of require function</param>
        /// <param name="action">Action to be evaluated as a result</param>
        public RequireCommand(
            Func<Require<TModel>, Require<TModel>> require,
            Action action)
            : base(action)
        {
            _requireFunction = require;
        }

        /// <summary>
        /// Constructs the object
        /// </summary>
        /// <param name="applicationModel">Application state used in evaluaion</param>
        /// <param name="require">Producer of require function</param>
        /// <param name="action">Action to be evaluated as a result</param>
        public RequireCommand(
            Func<Require<TModel>, Require<TModel>> require,
            Action<ApplicationState<TModel>> action)
            : base(action)
        {
            _requireFunction = require;
        }

        /// <summary>
        /// Evaluates require statements and final command
        /// </summary>
        /// <param name="args">Model used in evaluation</param>
        /// <returns>Result stream</returns>
        public override IObservable<Unit> Execute(ApplicationState<TModel> model)
        {
            var rf =_requireFunction(new Require<TModel>(model));
            return rf
                .EvaluateOnce()
                .Select(_ => Unit.Default)
                .Chain(() => 
                    Observable.When(
                            rf
                            .EvaluateAfter()
                            .Select(_ => Unit.Default)
                        .And(base.Execute(model))
                        .Then((u1, u2) => Unit.Default))
                    );
        }
    }
}

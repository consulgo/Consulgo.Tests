using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;

namespace Consulgo.Test.ReactiveBdd
{
    /// <summary>
    /// Evaluates given conditions
    /// </summary>
    /// <typeparam name="TModel">Model of the system under test</typeparam>
    public class Require<TModel>
        where TModel : IModel
    {
        private readonly List<Func<IObservable<ApplicationState<TModel>>>> _listOfRequireBeforeFuncs
            = new List<Func<IObservable<ApplicationState<TModel>>>>();
        private readonly List<Func<IObservable<ApplicationState<TModel>>>> _listOfRequireAfterFuncs
            = new List<Func<IObservable<ApplicationState<TModel>>>>();
        private readonly ApplicationState<TModel> _state;

        /// <summary>
        /// Constructs the Require evaluator
        /// </summary>
        /// <param name="applicationModel">Initial application state</param>
        public Require(ApplicationState<TModel> applicationModel)
        {
            _state = applicationModel;
        }

        /// <summary>
        /// Adds property to evaluation as Pre-Condition
        /// </summary>
        /// <typeparam name="T">Type of statement</typeparam>
        /// <param name="propertyExpression">Expression to get the property</param>
        /// <param name="resolver">Resolver implmentation</param>
        /// <returns>Constructed object</returns>
        public Require<TModel> Before<T>(
            Expression<Func<TModel, T>> propertyExpression,
            IPropertyResolver<TModel> resolver)
        {
            RegisterRequire<T>(_listOfRequireBeforeFuncs, propertyExpression, resolver);
            return this;
        }

        /// <summary>
        /// Adds property to evaluation as Post-Condition
        /// </summary>
        /// <typeparam name="T">Type of statement</typeparam>
        /// <param name="propertyExpression">Expression to get the property</param>
        /// <param name="resolver">Resolver implmentation</param>
        /// <returns>Constructed object</returns>
        public Require<TModel> After<T>(
            Expression<Func<TModel, T>> propertyExpression,
            IPropertyResolver<TModel> resolver)
        {
            RegisterRequire<T>(_listOfRequireAfterFuncs, propertyExpression, resolver);
            return this;
        }

        /// <summary>
        /// Evaluates all registered Require Before statements
        /// </summary>
        /// <returns>Result stream</returns>
        public IObservable<ApplicationState<TModel>> EvaluateOnce()
        {
            return Evaluate(_listOfRequireBeforeFuncs);
        }

        /// <summary>
        /// Evaluates all registered Require After statements
        /// </summary>
        /// <returns>Result stream</returns>
        public IObservable<ApplicationState<TModel>> EvaluateAfter()
        {
            return Evaluate(_listOfRequireAfterFuncs);
        }

        private void RegisterRequire<T>(
            List<Func<IObservable<ApplicationState<TModel>>>> list,
            Expression<Func<TModel, T>> propertyExpression,
            IPropertyResolver<TModel> resolver)
        {
            Func<IObservable<ApplicationState<TModel>>> f = () => resolver.Resolve<T>(_state, propertyExpression);
            list.Add(f);
        }

        private IObservable<ApplicationState<TModel>> Evaluate(List<Func<IObservable<ApplicationState<TModel>>>> list)
        {
            if (!list.Any())
            {
                return Observable.Return(_state);
            }

            return list.ChainAll();
        }
    }
}

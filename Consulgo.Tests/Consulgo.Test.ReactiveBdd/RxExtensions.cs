using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Consulgo.Test.ReactiveBdd
{
    /// <summary>
    /// Helper reactive operators.
    /// Imporant assumption: Stream completes
    /// </summary>
    public static class RxExtensions
    {
        /// <summary>
        /// Chains two observables from their producers
        /// </summary>
        /// <typeparam name="T">Type of message in observables</typeparam>
        /// <param name="previousProducer">Froducer of first observable</param>
        /// <param name="nextProducer">Producer of second observable</param>
        /// <returns>Chained execution observable</returns>
        public static Func<IObservable<T>> ChainFuns<T>(Func<IObservable<T>> previousProducer, Func<IObservable<T>> nextProducer)
        {
            return () => Observable.Defer(previousProducer).IsEmpty().SelectMany(Observable.Defer(nextProducer));
        }

        /// <summary>
        /// Chains two observables where second is producent
        /// </summary>
        /// <typeparam name="T">Type of message in observables</typeparam>
        /// <param name="previous">First observable</param>
        /// <param name="nextProducer">Producer of next observable</param>
        /// <returns>Chained execution observable</returns>
        public static IObservable<T> Chain<T>(this IObservable<T> previous, Func<IObservable<T>> nextProducer)
        {
            return previous.IsEmpty().SelectMany(Observable.Defer(nextProducer));
        }

        /// <summary>
        /// Chains given collection of observables
        /// </summary>
        /// <typeparam name="T">Type of message in observables</typeparam>
        /// <param name="observableProducers">Observables to chain</param>
        /// <returns>Chained execution observable</returns>
        public static IObservable<T> ChainAll<T>(this IEnumerable<IObservable<T>> observables)
        {
            return observables.Aggregate((currentChain, next) =>
                Chain(currentChain, () => next));
        }

        /// <summary>
        /// Chains given collection of observables
        /// </summary>
        /// <typeparam name="T">Type of message in observables</typeparam>
        /// <param name="observableProducers">Factories for observables to chain</param>
        /// <returns>Chained execution observable</returns>
        public static IObservable<T> ChainAll<T>(this IEnumerable<Func<IObservable<T>>> observableProducers)
        {
            return observableProducers.Aggregate((currentChain, next) =>
                ChainFuns(currentChain, next))();
        }
    }
}

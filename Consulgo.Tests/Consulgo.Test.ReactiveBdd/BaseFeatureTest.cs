using System;

namespace Consulgo.Test.ReactiveBdd
{
    /// <summary>
    /// Base class for feature tests
    /// </summary>
    public class BaseFeatureTest
    {
        /// <summary>
        /// Entry point for every acceptance criteria
        /// TODO tests
        /// </summary>
        /// <typeparam name="TModel">Model of the system under test</typeparam>
        /// <param name="command">Command to be evaluated in given step</param>
        /// <returns>Control state of evaluation</returns>
        protected ControlState<TModel> Given<TModel>(Func<ApplicationState<TModel>, ICommand<TModel>> command)
            where TModel : IModel
        {
            var newappState = new ApplicationState<TModel>();
            var newControlState = new ControlState<TModel>(newappState);
            newControlState.RegisterCommand(state => command(state), CommandType.Eval);
            return newControlState;
        }
    }
}

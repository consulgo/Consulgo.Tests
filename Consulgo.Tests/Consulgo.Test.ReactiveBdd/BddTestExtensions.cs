
namespace Consulgo.Test.ReactiveBdd
{
    /// <summary>
    /// Extensions to the control state to support basic operations on the model
    /// TODO tests & implementation & documentation
    /// </summary>
    public static class BddTestExtensions
    {
        public static ControlState<TModel> And<TModel>(
            this ControlState<TModel> controlState,
            System.Func<ApplicationState<TModel>, ICommand<TModel>> command)
            where TModel : IModel
        {
            return controlState.RegisterCommand(state => command(state), CommandType.And);
        }

        public static ControlState<TModel> Then<TModel>(
            this ControlState<TModel> controlState,
            System.Func<ApplicationState<TModel>, ICommand<TModel>> command)
            where TModel : IModel
        {
            return controlState.RegisterCommand(state => command(state), CommandType.Eval);
        }

        public static ControlState<TModel> When<TModel>(
            this ControlState<TModel> controlState,
            System.Func<ApplicationState<TModel>, ICommand<TModel>> command)
            where TModel : IModel
        {
            return controlState.RegisterCommand(state => command(state), CommandType.Eval);
        }

        public static ControlState<TModel> Or<TModel>(
            this ControlState<TModel> controlState,
            System.Func<ApplicationState<TModel>, ICommand<TModel>> command)
            where TModel : IModel
        {
            return controlState.RegisterCommand(state => command(state), CommandType.Or);
        }
    }
}


namespace Consulgo.Test.ReactiveBdd
{
    /// <summary>
    /// Application state holder
    /// </summary>
    /// <typeparam name="TModel">Model of the system under test</typeparam>
    public class ApplicationState<TModel>
        where TModel : IModel
    {
        /// <summary>
        /// Gets or sets reference to model of system under test 
        /// </summary>
        public TModel Model { get; set; }
    }
}

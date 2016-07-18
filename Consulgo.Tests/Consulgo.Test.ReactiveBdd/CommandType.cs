
namespace Consulgo.Test.ReactiveBdd
{
    /// <summary>
    /// Type of command
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// Logical conjuction operator [reserved]
        /// </summary>
        And,
        /// <summary>
        /// Logical sum operator [reserved]
        /// </summary>
        Or,
        /// <summary>
        /// Evaluation of the command operator
        /// </summary>
        Eval
    }
}

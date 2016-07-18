using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Consulgo.Test.ReactiveBdd
{
    /// <summary>
    /// Holds control state of requirements evaluation
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class ControlState<TModel> where TModel : IModel
    {
        private class CommandsToEvaluate
        {
            public CommandsToEvaluate(
                Func<ApplicationState<TModel>, ICommand<TModel>> command,
                CommandType type)
            {
                Command = command;
                CommandType = type;
            }

            public Func<ApplicationState<TModel>, ICommand<TModel>> Command { get; set; }

            public CommandType CommandType { get; set; }
        }

        private ApplicationState<TModel> _state;
        private List<CommandsToEvaluate> commandList = new List<CommandsToEvaluate>();

        /// <summary>
        /// Constructs the control state
        /// </summary>
        /// <param name="state">Initial state of the system under test</param>
        public ControlState(ApplicationState<TModel> state)
        {
            _state = state;
        }

        /// <summary>
        /// Comments registration for future processing
        /// </summary>
        /// <param name="command">Command to be regiesterd for future processing</param>
        /// <param name="type">Type of command</param>
        /// <returns>this</returns>
        public ControlState<TModel> RegisterCommand(Func<ApplicationState<TModel>, ICommand<TModel>> command, CommandType type)
        {
            commandList.Add(new CommandsToEvaluate(command, type));
            return this;
        }

        /// <summary>
        /// Main evaluation of all registered commands
        /// TODO test and processes
        /// </summary>
        /// <returns>Final result of the model</returns>
        public IObservable<TModel> Start()
        {
            return (commandList
                        .Select(cmdTe => cmdTe.Command)
                        .Select(cmdFun => cmdFun(_state))
                        .Select(cmd => cmd.Execute(_state)))
                    .ChainAll()
                    .Select(s => _state.Model)
                    .LastAsync();
        }
    }
}

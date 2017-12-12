using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Core.Commands
{
    public interface ICommandDispatcher
    {
        Task<CommandValidationResult> Submit<TCommand>(TCommand command) where TCommand : ICommand;
    }

    public class CommandDispatcher : ICommandDispatcher
    {
        private IDictionary<Type, object> _handlers = new ConcurrentDictionary<Type, object>();

        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            _handlers.Add(typeof(TCommand), handler);
        }

        public Task<CommandValidationResult> Submit<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandType = typeof(TCommand);

            if (_handlers.ContainsKey(commandType))
            {
                return ((ICommandHandler<TCommand>)_handlers[commandType]).Handle(command);
            }

            return Task.FromResult(new CommandValidationResult
            {
                Errors = new [] {$"Handler for command type {commandType} not found"}
            });
        }
    }
}

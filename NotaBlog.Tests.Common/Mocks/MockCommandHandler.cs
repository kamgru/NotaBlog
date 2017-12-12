using NotaBlog.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Tests.Common.Mocks
{
    public class MockCommand : ICommand
    {
        public Guid EntityId { get; set; }
    }

    public class MockCommandHandler : ICommandHandler<MockCommand>
    {
        public CommandValidationResult CommandValidationResult { get; set; }
        public bool WasHandled { get; private set; }

        public Task<CommandValidationResult> Handle(MockCommand command)
        {
            WasHandled = true;
            return Task.FromResult(CommandValidationResult ?? new CommandValidationResult());
        }
    }
}

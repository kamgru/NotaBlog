using System.Threading.Tasks;

namespace NotaBlog.Core.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<CommandValidationResult> Handle(TCommand command);
    }
}

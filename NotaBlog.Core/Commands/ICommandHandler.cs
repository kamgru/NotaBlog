namespace NotaBlog.Core.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        CommandValidationResult Handle(TCommand command);
    }
}

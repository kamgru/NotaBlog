using System;

namespace NotaBlog.Core.Commands
{
    public interface IEntityCommand : ICommand
    {
        Guid EntityId { get; set; }
    }
}

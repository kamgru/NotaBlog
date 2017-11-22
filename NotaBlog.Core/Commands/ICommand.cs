using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Commands
{
    public interface ICommand
    {
        Guid EntityId { get; set; }
    }
}

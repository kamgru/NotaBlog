using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Commands
{
    public class CreateStory : IEntityCommand
    {
        public Guid EntityId { get; set; }
    }
}

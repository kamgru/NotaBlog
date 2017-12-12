using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Commands
{
    public class SetSeName : IEntityCommand
    {
        public Guid EntityId { get; set; }
        public string SeName { get; set; }
    }
}

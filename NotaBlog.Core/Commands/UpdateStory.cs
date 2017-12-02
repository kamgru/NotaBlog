using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Commands
{
    public class UpdateStory : ICommand
    {
        public Guid EntityId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

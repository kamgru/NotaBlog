using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Commands
{
    public class CreateStory
    {
        public Guid StoryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

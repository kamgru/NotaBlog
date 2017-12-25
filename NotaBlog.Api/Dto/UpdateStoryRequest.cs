using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Api.Dto
{
    public class UpdateStoryRequest
    {
        public Guid StoryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

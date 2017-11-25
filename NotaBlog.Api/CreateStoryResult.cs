using System;

namespace NotaBlog.Api
{
    public class CreateStoryResult : Result
    {
        public Guid StoryId { get; set; }
    }
}

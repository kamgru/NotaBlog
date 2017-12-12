using System;

namespace NotaBlog.Api.Dto
{
    public class CreateStoryResult : Result
    {
        public Guid StoryId { get; set; }
    }
}

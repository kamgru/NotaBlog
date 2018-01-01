using System;

namespace NotaBlog.Api.Dto
{
    public class UpdateSeNameRequest
    {
        public Guid StoryId { get; set; }
        public string SeName { get; set; }
    }
}

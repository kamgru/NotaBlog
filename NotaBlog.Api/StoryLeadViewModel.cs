using System;

namespace NotaBlog.Api
{
    public class StoryLeadViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string LeadContent { get; set; }
        public DateTime Published { get; set; }
    }
}

using NotaBlog.Core.Entities;
using System;

namespace NotaBlog.Api.ViewModels
{
    public class StoryViewModel
    {
        public Guid Id { get; set; }
        public PublicationStatus PublicationStatus { get; set; }
        public DateTime Created { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
    }
}

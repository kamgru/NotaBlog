using NotaBlog.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Entities
{
    public class Story
    {
        public Guid Id { get; set; }
        public PublicationStatus PublicationStatus { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Published { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public static Story CreateNew(Guid id, IDateTimeProvider dateTimeProvider)
        {
            return new Story
            {
                Id = id,
                Created = dateTimeProvider.Now(),
                PublicationStatus = PublicationStatus.Draft
            };
        }

        public void Publish(IDateTimeProvider dateTimeProvider)
        {
            PublicationStatus = PublicationStatus.Published;
            Published = dateTimeProvider.Now();
        }
    }

    public enum PublicationStatus
    {
        Draft, Published, Withdrawn
    }
}

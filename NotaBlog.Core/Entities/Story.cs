using NotaBlog.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Entities
{
    public class Story
    {
        public Guid Id { get; private set; }
        public PublicationStatus PublicationStatus { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Published { get; private set; }
        public DateTime? Updated { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string SeName { get; private set; }

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

        public void Update(string title, string content, IDateTimeProvider dateTimeProvider)
        {
            Title = title;
            Content = content;
            Updated = dateTimeProvider.Now();
        }

        public void SetSeName(string seName)
        {
            SeName = seName;
        }
    }

    public enum PublicationStatus
    {
        Draft, Published, Withdrawn
    }
}

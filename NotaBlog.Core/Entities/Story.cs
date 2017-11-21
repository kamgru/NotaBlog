using NotaBlog.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Entities
{
    public class Story
    {
        public Guid Guid { get; private set; }
        public PublicationStatus PublicationStatus { get; private set; }
        public DateTime Created { get; private set; }

        private IDateTimeProvider _dateTimeProvider;

        public Story(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;

            Created = _dateTimeProvider.Now();
            PublicationStatus = PublicationStatus.Draft;
            Guid = Guid.NewGuid();
        }
    }

    public enum PublicationStatus
    {
        Draft, Published, Withdrawn
    }
}

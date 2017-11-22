using NotaBlog.Core.Entities;
using NotaBlog.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Factories
{
    public class StoryFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public StoryFactory(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public Story CreateNew(Guid guid)
        {
            return new Story
            {
                Id = guid,
                Created = _dateTimeProvider.Now()
            };
        }
    }
}

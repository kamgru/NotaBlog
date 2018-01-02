using NotaBlog.Core.Entities;
using NotaBlog.Core.Services;
using NotaBlog.Tests.Common.Mocks;
using System;

namespace NotaBlog.Core.Tests
{
    public class StoryTestsBase
    {
        protected readonly IDateTimeProvider _dateTimeProvider = new MockDateTimeProvider();

        protected Story CreateDefault()
        {
            var story = Entities.Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            return story;
        }

        protected Story CreatePublished()
        {
            var story = CreateDefault();
            story.Publish(_dateTimeProvider);
            return story;
        }
    }
}

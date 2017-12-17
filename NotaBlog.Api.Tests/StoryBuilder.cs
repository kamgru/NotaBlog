using NotaBlog.Core.Entities;
using NotaBlog.Tests.Common.Mocks;
using System;
using NotaBlog.Core.Services;

namespace NotaBlog.Api.Tests
{
    class StoryBuilder
    {
        private Story _story;
        private IDateTimeProvider _dateTimeProvider = new MockDateTimeProvider();

        public StoryBuilder Create()
        {
            _story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            return this;
        }

        public StoryBuilder WithTitle(string title)
        {
            _story.Update(title, _story.Content, _dateTimeProvider);
            return this;
        }

        public StoryBuilder WithContent(string content)
        {
            _story.Update(_story.Title, content, _dateTimeProvider);
            return this;
        }

        public StoryBuilder WithSeName(string seName)
        {
            _story.SetSeName(seName);
            return this;
        }

        public StoryBuilder Published()
        {
            _story.Publish(_dateTimeProvider);
            return this;
        }

        public StoryBuilder Default()
        {
            Create();
            WithTitle("story title");
            WithContent("story content");
            WithSeName("story-title");
            return this;
        }

        public StoryBuilder DefaultPublished()
        {
            Create();
            WithTitle("story title");
            WithContent("story content");
            WithSeName("story-title");
            Published();
            return this;
        }

        public Story Build()
        {
            return _story;
        }
    }
}

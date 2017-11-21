using FluentAssertions;
using NotaBlog.Core.Entities;
using NotaBlog.Core.Tests.Mocks;
using System;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class CreateStoryTests
    {
        private readonly StoryBuilder _builder = new StoryBuilder();
        private readonly DateTimeProvider _timeProvider = new DateTimeProvider();

        [Fact]
        public void WhenCreatingStory_ItShouldHaveStatusSetToDraft()
        {
            var story = _builder.CreateStory()
                .WithTimeProvider(_timeProvider)
                .Build();
            story.PublicationStatus.ShouldBeEquivalentTo(PublicationStatus.Draft);
        }

        [Fact]
        public void WhenCreatingStory_ItShouldHaveCreationDateSetToNow()
        {
            _timeProvider.DateTimeNow = DateTime.Now;

            var story = _builder.CreateStory()
                .WithTimeProvider(_timeProvider)
                .Build();

            story.Created.ShouldBeEquivalentTo(_timeProvider.Now());
        }

        [Fact]
        public void WhenCreatingStory_ItShouldHaveNewGuid()
        {
            var story = _builder.CreateStory()
                .WithTimeProvider(_timeProvider)
                .Build();

            story.Guid.Should().NotBe(Guid.Empty);
        }
    }
}

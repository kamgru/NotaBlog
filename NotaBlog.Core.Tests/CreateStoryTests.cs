using FluentAssertions;
using NotaBlog.Core.Entities;
using NotaBlog.Core.Factories;
using NotaBlog.Core.Tests.Mocks;
using System;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class CreateStoryTests
    {
        private readonly DateTimeProvider _timeProvider = new DateTimeProvider();

        [Fact]
        public void WhenCreatingStory_ItShouldHaveStatusSetToDraft()
        {
            var story = StoryFactory().CreateNew();
            story.PublicationStatus.ShouldBeEquivalentTo(PublicationStatus.Draft);
        }

        [Fact]
        public void WhenCreatingStory_ItShouldHaveCreationDateSetToNow()
        {
            _timeProvider.DateTimeNow = DateTime.Now;

            var story = StoryFactory().CreateNew();
            story.Created.ShouldBeEquivalentTo(_timeProvider.Now());
        }

        [Fact]
        public void WhenCreatingStory_ItShouldHaveNewGuid()
        {
            var story = StoryFactory().CreateNew();
            story.Guid.Should().NotBe(Guid.Empty);
        }

        private StoryFactory StoryFactory()
        {
            return new StoryFactory(_timeProvider);
        }
    }
}

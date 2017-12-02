using FluentAssertions;
using NotaBlog.Core.Entities;
using NotaBlog.Tests.Common.Mocks;
using System;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class StoryCreateNewTests
    {
        private readonly MockDateTimeProvider _timeProvider = new MockDateTimeProvider();

        [Fact]
        public void WhenCreatingStory_ItShouldHaveStatusSetToDraft()
        {
            var story = Story.CreateNew(Guid.NewGuid(), _timeProvider);
            story.PublicationStatus.ShouldBeEquivalentTo(PublicationStatus.Draft);
        }

        [Fact]
        public void WhenCreatingStory_ItShouldHaveCreationDateSetToNow()
        {
            _timeProvider.DateTimeNow = DateTime.Now;

            var story = Story.CreateNew(Guid.NewGuid(), _timeProvider);
            story.Created.ShouldBeEquivalentTo(_timeProvider.Now());
        }

        [Fact]
        public void WhenCreatingStory_ItShouldHaveNewGuid()
        {
            var story = Story.CreateNew(Guid.NewGuid(), _timeProvider);
            story.Id.Should().NotBe(Guid.Empty);
        }
    }
}

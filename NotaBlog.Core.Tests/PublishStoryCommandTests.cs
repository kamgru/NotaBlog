using FluentAssertions;
using NotaBlog.Core.Commands;
using NotaBlog.Core.Entities;
using NotaBlog.Tests.Common.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class PublishStoryCommandTests : PublishStoryCommandTestsBase
    {
        [Fact]
        public void GivenValidCommand_WhenPublishingStory_ItShouldSetPublicationStatusToPublished()
        {
            var story = new Story
            {
                Id = Guid.NewGuid(),
                Title = "title",
                Content = "content"
            };

            var repository = new InMemoryStoryRepository { Stories = new List<Story> { story } };

            Handler(repository)
                .Handle(new PublishStory
                {
                    EntityId = story.Id
                })
                .Wait();

            story.PublicationStatus.ShouldBeEquivalentTo(PublicationStatus.Published);
        }

        [Fact]
        public void GivenValidCommand_WhenStoryPublished_ItShouldBeUpdatedInRepository()
        {
            var story = new Story
            {
                Id = Guid.NewGuid(),
                Title = "title",
                Content = "content"
            };

            var repository = new InMemoryStoryRepository { Stories = new List<Story> { story } };

            Handler(repository)
                .Handle(new PublishStory
                {
                    EntityId = story.Id
                })
                .Wait();

            repository.UpdateWasCalled.Should().BeTrue();
        }

        [Fact]
        public void GivenValidCommand_WhenStoryPublished_ItShouldSetPublishedDate()
        {
            var story = new Story
            {
                Id = Guid.NewGuid(),
                Title = "title",
                Content = "content",
            };

            var repository = new InMemoryStoryRepository{ Stories = new List<Story> { story } };
            var dateTimeProvider = new MockDateTimeProvider
            {
                DateTimeNow = DateTime.Parse("2016-12-16 12:45")
            };

            Handler(repository, dateTimeProvider)
                .Handle(new PublishStory
                {
                    EntityId = story.Id
                })
                .Wait();

            story.Published.Should().BeCloseTo(dateTimeProvider.DateTimeNow);
        }
    }
}

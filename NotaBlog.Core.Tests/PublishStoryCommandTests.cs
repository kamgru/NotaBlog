using FluentAssertions;
using NotaBlog.Core.Commands;
using NotaBlog.Core.Entities;
using NotaBlog.Core.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class PublishStoryCommandTests
    {
        private readonly DateTimeProvider _dateTimeProvider = new DateTimeProvider();

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

            new PublishStoryHandler(repository, _dateTimeProvider)
                .Handle(new PublishStory
                {
                    EntityId = story.Id
                }).Wait();

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

            new PublishStoryHandler(repository, _dateTimeProvider)
                .Handle(new PublishStory
                {
                    EntityId = story.Id
                }).Wait();

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
                Published = null,
                PublicationStatus = PublicationStatus.Draft
            };

            var repository = new InMemoryStoryRepository{ Stories = new List<Story> { story } };
            _dateTimeProvider.DateTimeNow = DateTime.Parse("2016-12-16 12:45");

            new PublishStoryHandler(repository, _dateTimeProvider)
                .Handle(new PublishStory
                {
                    EntityId = story.Id
                }).Wait();

            story.Published.Should().BeCloseTo(_dateTimeProvider.DateTimeNow);
        }
    }
}

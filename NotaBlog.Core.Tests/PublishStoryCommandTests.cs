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

            new PublishStoryHandler(repository)
                .Handle(new PublishStory
                {
                    EntityId = story.Id
                });

            story.PublicationStatus.ShouldBeEquivalentTo(PublicationStatus.Published);
        }

        [Fact]
        public void GivenValidCommand_WhenStoryPublished_ItShouldBeSavedInRepository()
        {
            var story = new Story
            {
                Id = Guid.NewGuid(),
                Title = "title",
                Content = "content"
            };

            var repository = new InMemoryStoryRepository { Stories = new List<Story> { story } };

            new PublishStoryHandler(repository)
                .Handle(new PublishStory
                {
                    EntityId = story.Id
                });

            repository.UpdateWasCalled.Should().BeTrue();
            repository.SaveWasCalled.Should().BeTrue();
        }
    }
}

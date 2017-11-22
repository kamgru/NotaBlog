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
    public class PublishCommandValidationTests
    {
        [Fact]
        public void WhenStoryNotFound_ItShouldFail()
        {
            var commandHandler = new PublishStoryHandler(new InMemoryStoryRepository());
            
            var result = commandHandler.Handle(new PublishStory
            {
                EntityId = Guid.NewGuid()
            });

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void WhenStoryTitleIsEmpty_ItShouldFail()
        {
            var story = new Story
            {
                Id = Guid.NewGuid(),
                Title = string.Empty,
                Content = "content"
            };

            var repository = new InMemoryStoryRepository
            {
                Stories = new List<Story> { story }
            };

            var commandHandler = new PublishStoryHandler(repository);

            var result = commandHandler.Handle(new PublishStory
            {
                EntityId = story.Id
            });

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void WhenStoryContentIsEmpty_ItShouldFail()
        {
            var story = new Story
            {
                Id = Guid.NewGuid(),
                Title = "my title",
                Content = string.Empty
            };

            var repository = new InMemoryStoryRepository
            {
                Stories = new List<Story> { story }
            };

            var commandHandler = new PublishStoryHandler(repository);

            var result = commandHandler.Handle(new PublishStory
            {
                EntityId = story.Id
            });

            result.Success.Should().BeFalse();
        }

        [Theory]
        [InlineData(PublicationStatus.Published)]
        [InlineData(PublicationStatus.Withdrawn)]
        public void WhenStoryStatusIsNotDraft_ItShouldFail(PublicationStatus status)
        {
            var story = new Story
            {
                Id = Guid.NewGuid(),
                Title = "my title",
                Content = "content",
                PublicationStatus = status
            };

            var repository = new InMemoryStoryRepository
            {
                Stories = new List<Story> { story }
            };

            var commandHandler = new PublishStoryHandler(repository);

            var result = commandHandler.Handle(new PublishStory
            {
                EntityId = story.Id
            });

            result.Success.Should().BeFalse();
        }
    }
}

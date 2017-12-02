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
    public class PublishCommandValidationTests : PublishStoryCommandTestsBase
    {
        [Fact]
        public void WhenStoryNotFound_ItShouldFail()
        {
            var result = Handler()
                .Handle(new PublishStory
                {
                    EntityId = Guid.NewGuid()
                })
                .Result;

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

            var result = Handler(repository)
                .Handle(new PublishStory
                {
                    EntityId = story.Id
                })
                .Result;

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

            var result = Handler(repository)
                .Handle(new PublishStory
                {
                    EntityId = story.Id
                })
                .Result;

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void WhenStoryHasPublicationStatusSetToPublished_ItShouldFail()
        {
            var story = new Story
            {
                Id = Guid.NewGuid(),
                Title = "my title",
                Content = "content",
            };
            story.Publish(new MockDateTimeProvider());

            var repository = new InMemoryStoryRepository
            {
                Stories = new List<Story> { story }
            };

            var result = Handler(repository)
                .Handle(new PublishStory
                {
                    EntityId = story.Id
                })
                .Result;

            result.Success.Should().BeFalse();
        }
    }
}

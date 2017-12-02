using FluentAssertions;
using NotaBlog.Core.Commands;
using NotaBlog.Core.Entities;
using NotaBlog.Core.Services;
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
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            story.Title = "";
            story.Content = "content";

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
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            story.Title = "title";
            story.Content = "";

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
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            story.Title = "my title";
            story.Content = "my content";
            story.Publish(_dateTimeProvider);

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

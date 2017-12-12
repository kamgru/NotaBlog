using FluentAssertions;
using NotaBlog.Core.Commands;
using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using NotaBlog.Core.Services;
using NotaBlog.Tests.Common.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class UpdateStoryCommandTests
    {
        private readonly IDateTimeProvider _dateTimeProvider = new MockDateTimeProvider();

        [Fact]
        public void WhenStoryNotFound_ItShouldFail()
        {
            Handler(new InMemoryStoryRepository())
                .Handle(new UpdateStory
                {
                    EntityId = Guid.NewGuid(),
                    Title = "test",
                    Content = "test"
                })
                .Result.Success
                .Should().BeFalse();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenTitleEmpty_ItShouldFail(string title)
        {
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            var repository = new InMemoryStoryRepository
            {
                Stories = new List<Story> { story }
            };

            Handler(repository)
                .Handle(new UpdateStory
                {
                    EntityId = story.Id,
                    Title = title,
                    Content = "content"
                })
                .Result.Success
                .Should().BeFalse();
        }

        [Fact]
        public void GivenValidCommand_ItShouldUpdateStoryInRepository()
        {
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            var repository = new InMemoryStoryRepository
            {
                Stories = new List<Story> { story }
            };

            Handler(repository)
                .Handle(new UpdateStory
                {
                    EntityId = story.Id,
                    Title = "title"
                })
                .Wait();

            repository.UpdateWasCalled.Should().BeTrue();

        }

        [Fact]
        public void GivenValidCommand_WhenCreatingStory_ItShouldSetTitle()
        {
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            var repository = new InMemoryStoryRepository
            {
                Stories = new List<Story> { story }
            };
            var expectedTitle = "my test title";

            Handler(repository)
                .Handle(new UpdateStory
                {
                    EntityId = story.Id,
                    Title = expectedTitle
                })
                .Wait();

            repository.Stories.First().Title.ShouldBeEquivalentTo(expectedTitle);
        }

        [Fact]
        public void GivenValidCommand_WhenCreatingStory_ItShouldSetContent()
        {
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            var repository = new InMemoryStoryRepository
            {
                Stories = new List<Story> { story }
            };
            var expectedContent = "my test content";

            Handler(repository)
                .Handle(new UpdateStory
                {
                    EntityId = story.Id,
                    Title = "title",
                    Content = expectedContent
                })
                .Wait();

            repository.Stories.First().Content.ShouldBeEquivalentTo(expectedContent);
        }

        private UpdateStoryHandler Handler(IStoryRepository repository)
        {
            return new UpdateStoryHandler(repository, _dateTimeProvider);
        }
    }
}

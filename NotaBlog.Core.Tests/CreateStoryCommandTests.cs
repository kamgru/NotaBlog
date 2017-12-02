using FluentAssertions;
using NotaBlog.Core.Commands;
using NotaBlog.Tests.Common.Mocks;
using System;
using System.Linq;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class CreateStoryCommandTests : CreateStoryCommandTestsBase
    {
        [Fact]
        public void GivenValidCommand_WhenCreatingStory_ItShouldSetGuid()
        {
            var expectedId = Guid.NewGuid();
            var repository = new InMemoryStoryRepository();

            Handler(repository)
                .Handle(new CreateStory
                {
                    EntityId = expectedId
                })
                .Wait();

            repository.Stories.First().Id.ShouldBeEquivalentTo(expectedId);
        }

        [Fact]
        public void GivenValidCommand_WhenCreatingStory_ItShouldSetTitle()
        {
            var repository = new InMemoryStoryRepository();
            var expectedTitle = "my test title";

            Handler(repository)
                .Handle(new CreateStory
                {
                    EntityId = Guid.NewGuid(),
                    Title = expectedTitle
                })
                .Wait();

            repository.Stories.First().Title.ShouldBeEquivalentTo(expectedTitle);
        }

        [Fact]
        public void GivenValidCommand_WhenCreatingStory_ItShouldSetContent()
        {
            var repository = new InMemoryStoryRepository();
            var expectedContent = "my test content";

            Handler(repository)
                .Handle(new CreateStory
                {
                    EntityId = Guid.NewGuid(),
                    Title = "title",
                    Content = expectedContent
                })
                .Wait();

            repository.Stories.First().Content.ShouldBeEquivalentTo(expectedContent);
        }

        [Fact]
        public void GivenValidCommand_WhenStoryCreated_ItShouldBeAddedToRepository()
        {
            var repository = new InMemoryStoryRepository();

            Handler(repository)
                .Handle(new CreateStory())
                .Wait();

            repository.Stories.Should().HaveCount(1);
        }
    }
}

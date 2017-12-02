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
        public void WhenGuidEmpty_ItShouldFail()
        {
            Handler(new InMemoryStoryRepository())
                .Handle(new CreateStory
                {
                    EntityId = Guid.Empty
                })
                .Result.Success
                .Should().BeFalse();
        }

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
        public void GivenValidCommand_WhenStoryCreated_ItShouldBeAddedToRepository()
        {
            var repository = new InMemoryStoryRepository();

            Handler(repository)
                .Handle(new CreateStory
                {
                    EntityId = Guid.NewGuid()
                })
                .Wait();

            repository.Stories.Should().HaveCount(1);
        }
    }
}

using FluentAssertions;
using NotaBlog.Core.Commands;
using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using NotaBlog.Core.Services;
using NotaBlog.Tests.Common.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class SetSeNameCommandHandlerTests
    {
        private readonly IDateTimeProvider _dateTimeProvider = new MockDateTimeProvider();

        [Fact]
        public void WhenStoryNotFound_ItShouldFail()
        {
            Handler(new InMemoryStoryRepository())
                .Handle(new SetSeName
                {
                    EntityId = Guid.NewGuid(),
                    SeName = "test"
                })
                .Result.Success
                .Should().BeFalse();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenSeNameEmpty_ItShouldFail(string seName)
        {
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            var repository = new InMemoryStoryRepository { Stories = new List<Story> { story } };

            Handler(repository)
                .Handle(new SetSeName
                {
                    EntityId = story.Id,
                    SeName = seName
                })
                .Result.Success
                .Should().BeFalse();
        }

        [Fact]
        public void GivenValidCommand_ItShouldSetStorySeName()
        {
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            var repository = new InMemoryStoryRepository { Stories = new List<Story> { story } };

            Handler(repository)
                .Handle(new SetSeName
                {
                    EntityId = story.Id,
                    SeName = "test-search-engine-name-set"
                })
                .Result.Success
                .Should().BeTrue();

            story.SeName.ShouldBeEquivalentTo("test-search-engine-name-set");
        }

        [Fact]
        public void GivenValidCommand_WhenSeNameSet_ItShouldUpdateStoryInRepository()
        {
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            var repository = new InMemoryStoryRepository { Stories = new List<Story> { story } };

            Handler(repository)
                .Handle(new SetSeName
                {
                    EntityId = story.Id,
                    SeName = "test-search-engine-name-set"
                })
                .Result.Success
                .Should().BeTrue();

            repository.UpdateWasCalled.Should().BeTrue();
        }

        private SetSetNameCommandHandler Handler(IStoryRepository repository)
        {
            return new SetSetNameCommandHandler(repository);
        }
    }
}

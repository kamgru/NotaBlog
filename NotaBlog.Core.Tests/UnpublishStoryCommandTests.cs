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
    public class UnpublishStoryCommandTests
    {
        private readonly IDateTimeProvider _dateTimeProvider = new MockDateTimeProvider();

        [Fact]
        public void WhenStoryNotFound_ItShouldFail()
        {
            Handler()
                .Handle(new UnpublishStory
                {
                    EntityId = Guid.NewGuid()
                })
                .Result.Success
                .Should().BeFalse();
        }

        [Fact]
        public void WhenStoryStatusOtherThanPublished_ItShouldFail()
        {
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            var repository = new InMemoryStoryRepository { Stories = new List<Story> { story } };

            Handler(repository)
                .Handle(new UnpublishStory
                {
                    EntityId = story.Id
                })
                .Result.Success
                .Should().BeFalse();
        }

        [Fact]
        public void WhenCommandValid_ItShouldSetPublicationStatusToDraft()
        {
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            story.Publish(_dateTimeProvider);
            var repository = new InMemoryStoryRepository { Stories = new List<Story> { story } };

            Handler(repository)
                .Handle(new UnpublishStory
                {
                    EntityId = story.Id
                })
                .Result.Success
                .Should().BeTrue();

            story.PublicationStatus.ShouldBeEquivalentTo(PublicationStatus.Draft);
        }

        [Fact]
        public void WhenCommandValid_ItShouldUpdateStoryRepository()
        {
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            story.Publish(_dateTimeProvider);
            var repository = new InMemoryStoryRepository { Stories = new List<Story> { story } };

            Handler(repository)
                .Handle(new UnpublishStory
                {
                    EntityId = story.Id
                })
                .Result.Success
                .Should().BeTrue();

            repository.UpdateWasCalled.Should().BeTrue();
        }

        private UnpublishStoryHandler Handler(InMemoryStoryRepository repository = null)
        {
            repository = repository ?? new InMemoryStoryRepository();
            return new UnpublishStoryHandler(repository);
        }
    }
}

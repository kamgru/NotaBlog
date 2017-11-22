using FluentAssertions;
using NotaBlog.Core.Commands;
using NotaBlog.Core.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class CreateStoryCommandTests
    {
        private readonly DateTimeProvider _dateTimeProvider = new DateTimeProvider { DateTimeNow = DateTime.Now };

        [Fact]
        public void GivenValidCommand_WhenCreatingStory_ItShouldSetGuid()
        {
            var repository = new InMemoryStoryRepository();
            var commandHandler = new CommandHandler(repository, _dateTimeProvider);
            var expectedId = Guid.NewGuid();

            commandHandler.Handle(new CreateStory
            {
                StoryId = expectedId
            });

            repository.Stories.First().Id.ShouldBeEquivalentTo(expectedId);
        }

        [Fact]
        public void GivenValidCommand_WhenCreatingStory_ItShouldSetTitle()
        {
            var repository = new InMemoryStoryRepository();
            var commandHandler = new CommandHandler(repository, _dateTimeProvider);
            var expectedTitle = "my test title";

            commandHandler.Handle(new CreateStory
            {
                StoryId = Guid.NewGuid(),
                Title = expectedTitle
            });

            repository.Stories.First().Title.ShouldBeEquivalentTo(expectedTitle);
        }

        [Fact]
        public void GivenValidCommand_WhenCreatingStory_ItShouldSetContent()
        {
            var repository = new InMemoryStoryRepository();
            var commandHandler = new CommandHandler(repository, _dateTimeProvider);
            var expectedContent = "my test content";

            commandHandler.Handle(new CreateStory
            {
                StoryId = Guid.NewGuid(),
                Title = "title",
                Content = expectedContent
            });

            repository.Stories.First().Content.ShouldBeEquivalentTo(expectedContent);
        }

        [Fact]
        public void GivenValidCommand_WhenStoryCreated_ItShouldBeAddedToRepository()
        {
            var repository = new InMemoryStoryRepository();
            var commandHandler = new CommandHandler(repository, _dateTimeProvider);

            commandHandler.Handle(new CreateStory());

            repository.Stories.Should().HaveCount(1);
        }

        [Fact]
        public void GivenValidCommand_WhenStoryAddedToRepository_ItShouldSave()
        {
            var repository = new InMemoryStoryRepository();
            var commandHandler = new CommandHandler(repository, _dateTimeProvider);

            commandHandler.Handle(new CreateStory());

            repository.SaveWasCalled.Should().BeTrue();
        }
    }
}

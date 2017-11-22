using FluentAssertions;
using NotaBlog.Core.Commands;
using NotaBlog.Core.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class CommandHandlerTests
    {
        private readonly DateTimeProvider _dateTimeProvider = new DateTimeProvider { DateTimeNow = DateTime.Now };

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

using FluentAssertions;
using NotaBlog.Core.Commands;
using NotaBlog.Tests.Common.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class CommandDispatcherTests
    {
        [Fact]
        public void WhenHandlerNotFound_ItShouldFail()
        {
            var dispatcher = new CommandDispatcher();

            var result = dispatcher.Submit(new MockCommand()).Result;

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void ItShouldHandleCommand()
        {
            var dispatcher = new CommandDispatcher();
            var handler = new MockCommandHandler();

            dispatcher.RegisterHandler(handler);

            var result = dispatcher.Submit(new MockCommand()).Result;

            handler.WasHandled.Should().BeTrue();
        }

        [Fact]
        public void ItShouldReturnHandlerCommandValidationResult()
        {
            var commandValidationResult = new CommandValidationResult
            {
                Errors = new string[0]
            };

            var handler = new MockCommandHandler
            {
                CommandValidationResult = commandValidationResult
            };

            var dispatcher = new CommandDispatcher();
            dispatcher.RegisterHandler(handler);

            var result = dispatcher.Submit(new MockCommand()).Result;

            result.Should().BeSameAs(commandValidationResult);
        }
    }
}

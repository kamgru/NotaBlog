using FluentAssertions;
using NotaBlog.Api.Services;
using NotaBlog.Core.Commands;
using NotaBlog.Tests.Common.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NotaBlog.Api.Tests
{
    public class GetStoryHeadersTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenPageIsLessThanOne_ItShouldReturnEmptyCollection(int page)
        {
            var service = new StoryAdminService(new CommandDispatcher(), new InMemoryStoryRepository());

            var result = service.GetStoryHeaders(page, 10).Result;

            result.Items.Should().BeEmpty();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenCountIsLessThanOne_ItShouldReturnEmptyCollection(int count)
        {
            var service = new StoryAdminService(new CommandDispatcher(), new InMemoryStoryRepository());

            var result = service.GetStoryHeaders(1, count).Result;

            result.Items.Should().BeEmpty();
        }
    }
}

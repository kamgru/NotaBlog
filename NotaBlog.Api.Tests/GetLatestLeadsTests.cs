using FluentAssertions;
using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using NotaBlog.Tests.Common.Mocks;
using System;
using System.Collections.Generic;
using Xunit;
using System.Collections;

namespace NotaBlog.Api.Tests
{
    public class GetLatestLeadsTests
    {
        [Fact]
        public void ItShouldNotReturnNull()
        {
            var repository = new InMemoryStoryRepository
            {
                Stories = new List<Story>()
            };

            var storyService = new StoryService(repository);

            var result = storyService.GetLatestLeads(10).Result;

            result.Should().NotBeNull();
        }
    }
}

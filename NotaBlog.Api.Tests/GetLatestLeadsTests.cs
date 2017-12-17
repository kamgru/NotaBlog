using FluentAssertions;
using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using NotaBlog.Tests.Common.Mocks;
using System.Collections.Generic;
using Xunit;
using System.Collections;
using NotaBlog.Api.Services;

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

        [Fact]
        public void ItShouldReturnOnlyPublishedStories()
        {
            var repository = new InMemoryStoryRepository();
            repository.Stories.AddRange(NotPublishedStories(10));
            repository.Stories.AddRange(PublishedStories(6));

            var service = new StoryService(repository);

            var result = service.GetLatestLeads(20).Result;

            result.Should().HaveCount(6);
        }

        private IEnumerable<Story> PublishedStories(int count)
        {
            var builder = new StoryBuilder();
            var result = new List<Story>();
            for (var i = 0; i < count; i++)
            {
                result.Add(builder.Create().DefaultPublished().Build());
            }
            return result;
        }

        private IEnumerable<Story> NotPublishedStories(int count)
        {
            var builder = new StoryBuilder();
            var result = new List<Story>();
            for (var i = 0; i < count; i++)
            {
                result.Add(builder.Create().Default().Build());
            }
            return result;
        }
    }
}

using FluentAssertions;
using NotaBlog.Api.Services;
using NotaBlog.Core.Entities;
using NotaBlog.Tests.Common.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NotaBlog.Api.Tests
{
    public class GetPublishedStoryTests
    {
        [Fact]
        public void WhenStoryNotFound_ItShouldReturnNull()
        {
            var service = new StoryService(new InMemoryStoryRepository());

            var result = service.GetPublishedStory("story-does-not-exist").Result;

            result.Should().BeNull();
        }

        [Fact]
        public void WhenStoryPublicationStatusNotPublished_ItShouldReturnNull()
        {
            var repository = new InMemoryStoryRepository
            {
                Stories = new List<Story>
                {
                    new StoryBuilder().Create().Default().WithSeName("test-story").Build()
                }
            };

            var service = new StoryService(repository);

            var result = service.GetPublishedStory("test-story").Result;

            result.Should().BeNull();
        }

        [Fact]
        public void WhenStoryValid_ItShouldReturnViewModel()
        {
            var story = new StoryBuilder()
                .Create()
                .WithTitle("test title")
                .WithContent("test content")
                .WithSeName("test-name")
                .Published()
                .Build();

            var repository = new InMemoryStoryRepository { Stories = new List<Story> { story } };
            var service = new StoryService(repository);

            var result = service.GetPublishedStory("test-name").Result;

            result.Id.Should().Be(story.Id);
            result.Title.Should().BeEquivalentTo(story.Title);
            result.Content.Should().BeEquivalentTo(story.Content);
            result.Published.Should().BeCloseTo(story.Published.Value);
        }
    }
}

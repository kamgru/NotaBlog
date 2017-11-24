using FluentAssertions;
using MongoDB.Driver;
using NotaBlog.Core.Entities;
using NotaBlog.Core.Factories;
using NotaBlog.Core.Repositories;
using System;
using Xunit;

namespace NotaBlog.Persistence.Tests
{
    public class StoryRepositoryTests
    {
        private const string ConnectionString = "mongodb://localhost:27017";
        private const string Database = "NotaBlog_TEST";
        private const string Collection = "Stories";

        [Fact]
        public void TestAddStory()
        {
            var story = new Story
            {
                Id = Guid.NewGuid(),
                Content = "content",
                Title = "title",
                Created = DateTime.UtcNow,
                PublicationStatus = PublicationStatus.Published
            };

            var database = new MongoClient(ConnectionString)
                .GetDatabase(Database);
            database.DropCollection(Collection);

            new StoryRepository(database)
                .Add(story)
                .Wait();

            var collection = database.GetCollection<Story>(Collection);
            collection.Count("{}").Should().Be(1);
            var result = collection.Find(x => x.Id == story.Id).FirstOrDefault();
            result.Should().BeEquivalentTo(story, e => e.Excluding(p => p.Created));
            result.Created.Should().BeCloseTo(story.Created);
        }

        [Fact]
        public void TestGetStory()
        {
            var story = new Story
            {
                Id = Guid.NewGuid(),
                Content = "content",
                Title = "title",
                Created = DateTime.UtcNow,
                PublicationStatus = PublicationStatus.Published
            };

            var database = new MongoClient(ConnectionString)
                .GetDatabase(Database);
            database.DropCollection(Collection);
            database.GetCollection<Story>(Collection).InsertOne(story);

            var result = new StoryRepository(database)
                .Get(story.Id)
                .Result;

            result.Should().BeEquivalentTo(story, e => e.Excluding(p => p.Created));
            result.Created.Should().BeCloseTo(story.Created);
        }

        [Fact]
        public void TestUpdateStory()
        {
            var story = new Story
            {
                Id = Guid.NewGuid(),
                Content = "content",
                Title = "title",
                Created = DateTime.UtcNow,
                PublicationStatus = PublicationStatus.Published
            };

            var database = new MongoClient(ConnectionString)
                .GetDatabase(Database);
            database.DropCollection(Collection);
            database.GetCollection<Story>(Collection).InsertOne(story);

            story.Content = "updated content";
            story.Title = "updated title";

            var repository = new StoryRepository(database);
            repository.Update(story).Wait();

            var result = repository.Get(story.Id).Result;
            result.Should().BeEquivalentTo(story, x => x.Excluding(p => p.Created));
            result.Created.Should().BeCloseTo(story.Created);
        }
    }
}

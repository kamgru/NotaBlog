using FluentAssertions;
using MongoDB.Driver;
using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using NotaBlog.Core.Services;
using NotaBlog.Tests.Common.Mocks;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace NotaBlog.Persistence.Tests
{
    public class StoryRepositoryTests
    {
        private const string ConnectionString = "mongodb://localhost:27017";
        private const string Database = "NotaBlog_TEST";
        private const string Collection = "Stories";

        private readonly IDateTimeProvider _dateTimeProvider = new MockDateTimeProvider { DateTimeNow = DateTime.UtcNow };

        [Fact]
        public void TestAddStory()
        {
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            story.Update("title", "content", _dateTimeProvider);
            story.Publish(_dateTimeProvider);

            var database = new MongoClient(ConnectionString)
                .GetDatabase(Database);
            database.DropCollection(Collection);

            new StoryRepository(database)
                .Add(story)
                .Wait();

            var collection = database.GetCollection<Story>(Collection);
            collection.Count("{}").Should().Be(1);
            var result = collection.Find(x => x.Id == story.Id).FirstOrDefault();
            result.Should().BeEquivalentTo(
                story, e => e.Excluding(s => s.Created)
                .Excluding(s => s.Published)
                .Excluding(s => s.Updated));
            result.Created.Should().BeCloseTo(story.Created);
            result.Published.Should().BeCloseTo(story.Published.Value);
        }

        [Fact]
        public void TestGetStory()
        {
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            story.Update("title", "content", _dateTimeProvider);
            story.Publish(_dateTimeProvider);

            var database = new MongoClient(ConnectionString)
                .GetDatabase(Database);
            database.DropCollection(Collection);
            database.GetCollection<Story>(Collection).InsertOne(story);

            var result = new StoryRepository(database)
                .Get(story.Id)
                .Result;

            result.Should().BeEquivalentTo(
                story, e => e.Excluding(p => p.Created)
                    .Excluding(p => p.Published)
                    .Excluding(p => p.Updated));
            result.Created.Should().BeCloseTo(story.Created);
            result.Published.Should().BeCloseTo(story.Published.Value);
        }

        [Fact]
        public void TestUpdateStory()
        {
            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            story.Update("title", "content", _dateTimeProvider);
            story.Publish(_dateTimeProvider);

            var database = new MongoClient(ConnectionString)
                .GetDatabase(Database);
            database.DropCollection(Collection);
            database.GetCollection<Story>(Collection).InsertOne(story);

            story.Update("updated title", "updated content", _dateTimeProvider);

            var repository = new StoryRepository(database);
            repository.Update(story).Wait();

            var result = repository.Get(story.Id).Result;
            result.Should().BeEquivalentTo(
                story, e => e.Excluding(p => p.Created)
                    .Excluding(p => p.Published)
                    .Excluding(p => p.Updated));
            result.Created.Should().BeCloseTo(story.Created);
            result.Published.Should().BeCloseTo(story.Published.Value);
            result.Updated.Should().BeCloseTo(story.Updated.Value);
        }

        [Fact]
        public void TestGetMultiple()
        {
            var database = new MongoClient(ConnectionString)
                .GetDatabase(Database);
            database.DropCollection(Collection);

            var story1 = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            story1.Update("title", "content", _dateTimeProvider);
            database.GetCollection<Story>(Collection).InsertOne(story1);

            var story2 = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            story2.Update("title", "content", _dateTimeProvider);
            database.GetCollection<Story>(Collection).InsertOne(story2);

            var story3 = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            story3.Update("different title", "content", _dateTimeProvider);
            database.GetCollection<Story>(Collection).InsertOne(story3);

            var repository = new StoryRepository(database);

            repository.Get(x => x.Id != Guid.Empty).Result.Should().HaveCount(3);
            repository.Get(x => x.Title == "title").Result.Should().HaveCount(2);
        }

        [Fact]
        public void WhenFilterNull_ItShouldThrowException()
        {
            var repository = new StoryRepository(new MongoClient(ConnectionString).GetDatabase(Database));
            Func<Task> test = async () => await repository.Get(filter: null);
            test.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void WhenPredicateNull_ItShouldReturnAllEntities()
        {
            var database = new MongoClient(ConnectionString)
                .GetDatabase(Database);
            database.DropCollection(Collection);
            database.GetCollection<Story>(Collection).InsertOne(new Story());
            database.GetCollection<Story>(Collection).InsertOne(new Story());
            database.GetCollection<Story>(Collection).InsertOne(new Story());
            database.GetCollection<Story>(Collection).InsertOne(new Story());
            database.GetCollection<Story>(Collection).InsertOne(new Story());

            var repository = new StoryRepository(database);

            var result = repository.Get(new StoryFilter()).Result;

            result.TotalCount.Should().Be(5);
        }

        [Fact]
        public void TestGetPaginated()
        {
            var database = new MongoClient(ConnectionString)
                .GetDatabase(Database);
            database.DropCollection(Collection);
            database.GetCollection<Story>(Collection).InsertOne(new Story());
            database.GetCollection<Story>(Collection).InsertOne(new Story());
            database.GetCollection<Story>(Collection).InsertOne(new Story());
            database.GetCollection<Story>(Collection).InsertOne(new Story());
            database.GetCollection<Story>(Collection).InsertOne(new Story());

            var repository = new StoryRepository(database);

            var filter = new StoryFilter
            {
                Page = 1,
                Count = 2,
                Predicate = story => story.Id != Guid.Empty
            };

            var result = repository.Get(filter).Result;
            result.TotalCount.Should().Be(5);
            result.Items.Should().HaveCount(2);
        }

        [Fact]
        public void TestGetBySeName()
        {
            var database = new MongoClient(ConnectionString).GetDatabase(Database);
            database.DropCollection(Collection);

            var story = Story.CreateNew(Guid.NewGuid(), _dateTimeProvider);
            story.SetSeName("test-story");

            database.GetCollection<Story>(Collection).InsertOne(story);

            var result = new StoryRepository(database).Get("test-story").Result;

            result.Should().BeEquivalentTo(story, x => x.Excluding(e => e.Created));
            result.Created.Should().BeCloseTo(story.Created);
        }
    }
}

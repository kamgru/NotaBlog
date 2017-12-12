using FluentAssertions;
using MongoDB.Driver;
using NotaBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NotaBlog.Persistence.Tests
{
    public class SettingsRepositoryTests
    {
        private const string ConnectionString = "mongodb://localhost:27017";
        private const string Database = "NotaBlog_TEST";
        private const string Collection = SettingsRepository.CollectionName;

        [Fact]
        public void WhenBlogSettingsDoesNotExist_ItShouldReturnEmptyBlogInfo()
        {
            var database = GetDatabase();
            database.DropCollection(Collection);

            var result = new SettingsRepository(database)
                .GetBlogInfo().Result;

            result.Should().NotBeNull();
            result.Title.Should().BeEmpty();
            result.Description.Should().BeEmpty();
        }

        [Fact]
        public void ItShouldReturnBlogInfo()
        {
            var blogInfo = new BlogInfo
            {
                Title = "test blog",
                Description = "blog description"
            };

            var database = GetDatabase();
            database.DropCollection(Collection);

            var collection = database.GetCollection<SettingsRepository.Setting>(Collection);
            collection.InsertOne(new SettingsRepository.Setting
            {
                Key = SettingsRepository.BlogInfoKey,
                Value = blogInfo
            });

            var result = new SettingsRepository(database).GetBlogInfo().Result;

            result.Should().BeEquivalentTo(blogInfo);
        }
        
        [Fact]
        public void GivenNullBlogInfo_WhenUpdating_ItShouldThrowException()
        {
            Func<Task> test = async () => await new SettingsRepository(GetDatabase()).UpdateBlogInfo(null);
            test.Should().Throw<ArgumentNullException>(); 
        }

        [Fact]
        public void ItShouldUpdateBlogInfo()
        {
            var database = GetDatabase();
            database.DropCollection(Collection);
            var collection = database.GetCollection<SettingsRepository.Setting>(Collection);
            collection.InsertOne(new SettingsRepository.Setting { Key = SettingsRepository.BlogInfoKey, Value = new BlogInfo() });

            new SettingsRepository(database)
                .UpdateBlogInfo(new BlogInfo
                {
                    Title = "updated blog title",
                    Description = "updated blog description"
                })
                .Wait();

            var result = collection.Find(x => x.Key == SettingsRepository.BlogInfoKey)
                .SingleOrDefault()?.Value as BlogInfo;

            result.Should().NotBeNull();
            result.Title.Should().BeEquivalentTo("updated blog title");
            result.Description.Should().BeEquivalentTo("updated blog description");
        }

        private IMongoDatabase GetDatabase()
        {
            return new MongoClient(ConnectionString)
                .GetDatabase(Database);
        }
    }
}

using FluentAssertions;
using MongoDB.Driver;
using NotaBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace NotaBlog.Persistence.Tests
{
    public class SettingsRepositoryTests
    {
        private const string ConnectionString = "mongodb://localhost:27017";
        private const string Database = "NotaBlog_TEST";
        private const string Collection = "Settings";

        [Fact]
        public void WhenBlogSettingsDoesNotExist_ItShouldReturnEmptyBlogInfo()
        {
            var database = new MongoClient(ConnectionString)
                .GetDatabase(Database);
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

            var database = new MongoClient(ConnectionString)
                .GetDatabase(Database);
            database.DropCollection(Collection);

            var collection = database.GetCollection<SettingsRepository.Setting>(Collection);
            collection.InsertOne(new SettingsRepository.Setting
            {
                Key = "blogInfo",
                Value = blogInfo
            });

            var result = new SettingsRepository(database).GetBlogInfo().Result;

            result.Should().BeEquivalentTo(blogInfo);
        }
        
    }
}

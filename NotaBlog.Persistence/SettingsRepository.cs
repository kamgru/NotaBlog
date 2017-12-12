using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Persistence
{
    public class SettingsRepository : ISettingsRepository
    {
        public const string CollectionName = "Settings";
        public const string BlogInfoKey = "BlogInfo";

        private readonly IMongoDatabase _database;

        public SettingsRepository(IMongoDatabase mongoDatabase)
        {
            _database = mongoDatabase;
        }

        public async Task<BlogInfo> GetBlogInfo()
        {
            var blogInfo = await _database.GetCollection<Setting>(CollectionName)
                .Find(x => x.Key == BlogInfoKey)
                .FirstOrDefaultAsync();

            return blogInfo?.Value as BlogInfo ?? new BlogInfo();
        }

        public async Task UpdateBlogInfo(BlogInfo blogInfo)
        {
            if (blogInfo == null)
            {
                throw new ArgumentNullException(nameof(blogInfo));
            }

            var setting = new Setting
            {
                Key = BlogInfoKey,
                Value = blogInfo
            };

            var collection = _database.GetCollection<Setting>(CollectionName);
            await collection.ReplaceOneAsync(x => x.Key == BlogInfoKey, setting);
        }

        public class Setting
        {
            [BsonId]
            [BsonRepresentation(BsonType.String)]
            public string Key { get; set; }
            public object Value { get; set; }
        }
    }
}

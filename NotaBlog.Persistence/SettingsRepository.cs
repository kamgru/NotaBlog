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
        private readonly IMongoDatabase _database;

        public SettingsRepository(IMongoDatabase mongoDatabase)
        {
            _database = mongoDatabase;
        }

        public async Task<BlogInfo> GetBlogInfo()
        {
            var blogInfo = await _database.GetCollection<Setting>("Settings")
                .Find(x => x.Key == "blogInfo")
                .FirstOrDefaultAsync();

            return blogInfo?.Value as BlogInfo ?? new BlogInfo();
        }

        public Task UpdateBlogInfo(BlogInfo blogInformation)
        {
            throw new NotImplementedException();
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

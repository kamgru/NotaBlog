using NotaBlog.Core.Repositories;
using System;
using NotaBlog.Core.Entities;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace NotaBlog.Persistence
{
    public class StoryRepository : IStoryRepository
    {
        private readonly IMongoDatabase _database;

        public StoryRepository(IMongoDatabase mongoDatabase)
        {
            _database = mongoDatabase;
        }

        public async Task Add(Story story)
        {
            await GetCollection().InsertOneAsync(story);
        }

        public Task<Story> Get(Guid id)
        {
            return GetCollection().Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(Story story)
        {
            await GetCollection().ReplaceOneAsync(x => x.Id == story.Id, story);
        }

        private IMongoCollection<Story> GetCollection()
        {
            return _database.GetCollection<Story>("Stories"); 
        }
    }
}

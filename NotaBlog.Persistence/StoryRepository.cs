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
            var collection = _database.GetCollection<Story>("Stories");
            await collection.InsertOneAsync(story);
        }

        public Task<Story> Get(Guid id)
        {
            var collection = _database.GetCollection<Story>("Stories");
            return collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task Update(Story story)
        {
            throw new NotImplementedException();
        }
    }
}

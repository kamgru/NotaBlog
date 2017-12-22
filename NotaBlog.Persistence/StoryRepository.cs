using NotaBlog.Core.Repositories;
using System;
using NotaBlog.Core.Entities;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        public Task<Story> Get(string seName)
        {
            return GetCollection().Find(x => x.SeName == seName).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Story>> Get(Expression<Func<Story, bool>> predicate)
        {
            return await GetCollection()
                .Find(predicate)
                .ToListAsync();
        }

        public async Task<PaginatedResult<Story>> Get(StoryFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var stories = filter.Predicate == null 
                ? GetCollection().Find(_ => true)
                : GetCollection().Find(filter.Predicate);

            if (filter.SortBy != null)
            {
                stories = filter.DescendingOrder
                    ? stories.SortByDescending(filter.SortBy)
                    : stories.SortBy(filter.SortBy);
            }

            var count = await stories.CountAsync();
            var items = await stories.Skip((filter.Page - 1) * filter.Count)
                .Limit(filter.Count)
                .ToListAsync();

            return new PaginatedResult<Story>
            {
                TotalCount = count,
                Items = items
            };
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
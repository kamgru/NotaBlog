using NotaBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using NotaBlog.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace NotaBlog.Core.Tests.Mocks
{
    class InMemoryStoryRepository : IStoryRepository
    {
        public List<Story> Stories { get; set; } = new List<Story>();
        public bool UpdateWasCalled { get; private set; }

        public Task Add(Story story)
        {
            Stories.Add(story);
            return Task.FromResult(0);
        }

        public Task<Story> Get(Guid id)
        {
            return Task.FromResult(Stories.FirstOrDefault(item => item.Id == id));
        }

        public Task<IEnumerable<Story>> Get(Expression<Func<Story, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<Story>> Get(StoryFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task Update(Story story)
        {
            var oldStory = Stories.FirstOrDefault(item => item.Id == story.Id);
            Stories.Remove(oldStory);
            Stories.Add(story);
            UpdateWasCalled = true;
            return Task.FromResult(0);
        }
    }
}

using NotaBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Core.Repositories
{
    public interface IStoryRepository
    {
        Task Add(Story story);
        Task<Story> Get(Guid id);
        Task<Story> Get(string seName);
        Task Update(Story story);
        Task<IEnumerable<Story>> Get(Expression<Func<Story, bool>> predicate);
        Task<PaginatedResult<Story>> Get(StoryFilter filter);
    }
}

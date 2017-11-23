using NotaBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Core.Repositories
{
    public interface IStoryRepository
    {
        Task Add(Story story);
        Task<Story> Get(Guid id);
        Task Update(Story story);
    }
}

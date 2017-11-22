using NotaBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Repositories
{
    public interface IStoryRepository
    {
        void Add(Story story);
        Story Get(Guid id);
        void Update(Story story);
        void Save();
    }
}

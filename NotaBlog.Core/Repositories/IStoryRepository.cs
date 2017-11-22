using NotaBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Repositories
{
    public interface IStoryRepository
    {
        void Add(Story story);
        void Save();
    }
}

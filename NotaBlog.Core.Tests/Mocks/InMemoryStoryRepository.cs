using NotaBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using NotaBlog.Core.Entities;
using System.Linq;

namespace NotaBlog.Core.Tests.Mocks
{
    class InMemoryStoryRepository : IStoryRepository
    {
        public List<Story> Stories { get; set; } = new List<Story>();
        public bool SaveWasCalled { get; set; }

        public void Add(Story story)
        {
            Stories.Add(story);
        }

        public Story Get(Guid id)
        {
            return Stories.FirstOrDefault(item => item.Id == id);
        }

        public void Save()
        {
            SaveWasCalled = true;
        }
    }
}

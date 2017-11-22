using NotaBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using NotaBlog.Core.Entities;

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

        public void Save()
        {
            SaveWasCalled = true;
        }
    }
}

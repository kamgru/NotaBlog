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
        public bool UpdateWasCalled { get; private set; }

        public void Add(Story story)
        {
            Stories.Add(story);
        }

        public Story Get(Guid id)
        {
            return Stories.FirstOrDefault(item => item.Id == id);
        }

        public void Update(Story story)
        {
            var oldStory = Stories.FirstOrDefault(item => item.Id == story.Id);
            Stories.Remove(oldStory);
            Stories.Add(story);
            UpdateWasCalled = true;
        }
    }
}

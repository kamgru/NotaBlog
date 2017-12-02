using NotaBlog.Core.Repositories;
using System;
using Xunit;

namespace NotaBlog.Api.Tests
{
    public class GetLatestLeadsTests
    {
        [Fact]
        public void ItShouldNotReturnNull()
        {

        }
    }

    public class StoryServiceTestsBase
    {
        protected StoryService StoryService(IStoryRepository repository = null)
        {
            return null;
        } 
    }
}

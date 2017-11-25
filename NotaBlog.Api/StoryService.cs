using NotaBlog.Core.Commands;
using System;
using System.Threading.Tasks;

namespace NotaBlog.Api
{
    public class StoryService
    {
        private readonly CreateStoryHandler _createStoryHandler;

        public StoryService(CreateStoryHandler createStoryHandler)
        {
            _createStoryHandler = createStoryHandler;
        }

        public async Task<CreateStoryResult> CreateStory(string title, string content)
        {
            var command = new CreateStory
            {
                EntityId = Guid.NewGuid(),
                Title = title,
                Content = content
            };

            var validationResult = _createStoryHandler.Handle(command);

            return new CreateStoryResult
            {
                Success = validationResult.Success,
                StoryId = command.EntityId
            };
        }
    }
}

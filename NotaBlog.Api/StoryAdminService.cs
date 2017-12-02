using NotaBlog.Core.Commands;
using System;
using System.Threading.Tasks;

namespace NotaBlog.Api
{
    public class StoryAdminService
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public async Task<CreateStoryResult> CreateStory(string title, string content)
        {
            var command = new CreateStory
            {
                EntityId = Guid.NewGuid(),
                Title = title,
                Content = content
            };

            var validationResult = await _commandDispatcher.Submit(command);

            return new CreateStoryResult
            {
                Success = validationResult.Success,
                StoryId = command.EntityId
            };
        }

        public async Task<PublishStoryResult> PublishStory(Guid storyId)
        {
            var validationResult = await _commandDispatcher.Submit(new PublishStory
            {
                EntityId = storyId
            });

            return new PublishStoryResult
            {
                StoryId = storyId,
                Success = validationResult.Success
            };
        }
    }
}

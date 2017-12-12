using NotaBlog.Api.Dto;
using NotaBlog.Core.Commands;
using System;
using System.Threading.Tasks;

namespace NotaBlog.Api.Services
{
    public class StoryAdminService
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public StoryAdminService(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task<CreateStoryResult> CreateStory()
        {
            var command = new CreateStory
            {
                EntityId = Guid.NewGuid(),
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

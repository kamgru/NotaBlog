using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace NotaBlog.Core.Commands
{
    public class UnpublishStoryHandler : ICommandHandler<UnpublishStory>
    {
        private readonly IStoryRepository _storyRepository;

        public UnpublishStoryHandler(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public async Task<CommandValidationResult> Handle(UnpublishStory command)
        {
            var story = await _storyRepository.Get(command.EntityId);
            if (story == null)
            {
                return new CommandValidationResult("Story not found");
            }

            if (story.PublicationStatus != PublicationStatus.Published)
            {
                return new CommandValidationResult("Invalid status");
            }

            story.Unpublish();
            await _storyRepository.Update(story);

            return new CommandValidationResult();
        }
    }
}

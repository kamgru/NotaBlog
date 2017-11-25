using NotaBlog.Core.Entities;
using NotaBlog.Core.Factories;
using NotaBlog.Core.Repositories;
using NotaBlog.Core.Services;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Core.Commands
{
    public class CreateStoryHandler : ICommandHandler<CreateStory>
    {
        private IStoryRepository _storyRepository;
        private IDateTimeProvider _dateTimeProvider;

        public CreateStoryHandler(IStoryRepository storyRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _storyRepository = storyRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<CommandValidationResult> Handle(CreateStory command)
        {
            var story = new StoryFactory(_dateTimeProvider).CreateNew(command.EntityId);
            story.Title = command.Title;
            story.Content = command.Content;

            await _storyRepository.Add(story);

            return new CommandValidationResult();
        }
    }
}

using NotaBlog.Core.Entities;
using NotaBlog.Core.Factories;
using NotaBlog.Core.Repositories;
using NotaBlog.Core.Services;
using System.Collections.Generic;
using System.Text;

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

        public CommandValidationResult Handle(CreateStory command)
        {
            var story = new StoryFactory(_dateTimeProvider).CreateNew(command.EntityId);
            story.Title = command.Title;
            story.Content = command.Content;

            _storyRepository.Add(story);
            _storyRepository.Save();

            return new CommandValidationResult();
        }
    }
}

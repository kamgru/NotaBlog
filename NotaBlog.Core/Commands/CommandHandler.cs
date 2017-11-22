using NotaBlog.Core.Entities;
using NotaBlog.Core.Factories;
using NotaBlog.Core.Repositories;
using NotaBlog.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Commands
{
    public class CommandHandler
    {
        private IStoryRepository _storyRepository;
        private IDateTimeProvider _dateTimeProvider;

        public CommandHandler(IStoryRepository storyRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _storyRepository = storyRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public void Handle(CreateStory command)
        {
            var story = new StoryFactory(_dateTimeProvider).CreateNew(command.StoryId);
            _storyRepository.Add(story);
            _storyRepository.Save();
        }
    }
}

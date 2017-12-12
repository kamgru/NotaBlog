using NotaBlog.Core.Repositories;
using NotaBlog.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Core.Commands
{
    public class UpdateStoryHandler : ICommandHandler<UpdateStory>
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateStoryHandler(IStoryRepository storyRepository, IDateTimeProvider dateTimeProvider)
        {
            _storyRepository = storyRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<CommandValidationResult> Handle(UpdateStory command)
        {
            var story = await _storyRepository.Get(command.EntityId);

            if (story == null)
            {
                return new CommandValidationResult
                {
                    Errors = new[] { "Story not found" }
                };
            }

            if (string.IsNullOrEmpty(command.Title))
            {
                return new CommandValidationResult
                {
                    Errors = new[] { "Title not set" }
                };
            }

            story.Update(command.Title, command.Content, _dateTimeProvider);

            await _storyRepository.Update(story);

            return new CommandValidationResult();
        }
    }
}

using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using NotaBlog.Core.Services;
using System;
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
            if (command.EntityId == Guid.Empty)
            {
                return new CommandValidationResult
                {
                    Errors = new[] { "EntityId not set" }
                };
            }

            var story = Story.CreateNew(command.EntityId, _dateTimeProvider);

            await _storyRepository.Add(story);

            return new CommandValidationResult();
        }
    }
}

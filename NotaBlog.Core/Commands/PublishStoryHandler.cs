using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using NotaBlog.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotaBlog.Core.Commands
{
    public class PublishStoryHandler : ICommandHandler<PublishStory>
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public PublishStoryHandler(IStoryRepository storyRepository, IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            _storyRepository = storyRepository;
        }

        public async Task<CommandValidationResult> Handle(PublishStory command)
        {
            var story = await _storyRepository.Get(command.EntityId);

            if (!IsValid(command, story, out IEnumerable<string> errors))
            {
                return new CommandValidationResult(errors.ToArray());
            }

            story.PublicationStatus = PublicationStatus.Published;
            story.Published = _dateTimeProvider.Now();

            await _storyRepository.Update(story);

            return new CommandValidationResult();
        }

        private bool IsValid(PublishStory command, Story story, out IEnumerable<string> errors)
        {
            if (story == null)
            {
                errors = new[] { "Story not found" };
                return false;
            }

            errors = Enumerable.Empty<string>();

            if (string.IsNullOrEmpty(story.Title))
            {
                errors = errors.Concat(new[] { "Story title must be set" });
            }

            if (string.IsNullOrEmpty(story.Content))
            {
                errors = errors.Concat(new[] { "Story content must be set" });
            }

            if (story.PublicationStatus != PublicationStatus.Draft)
            {
                errors = errors.Concat(new[] { "You can only publish draft stories" });
            }

            return !errors.Any();
        }
    }
}

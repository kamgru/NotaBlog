using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotaBlog.Core.Commands
{
    public class PublishStoryHandler : ICommandHandler<PublishStory>
    {
        private readonly IStoryRepository _storyRepository;

        public PublishStoryHandler(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public CommandValidationResult Handle(PublishStory command)
        {
            var story = _storyRepository.Get(command.EntityId).Result;

            if (!IsValid(command, story, out IEnumerable<string> errors))
            {
                return new CommandValidationResult(errors.ToArray());
            }

            story.PublicationStatus = PublicationStatus.Published;

            _storyRepository.Update(story);

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

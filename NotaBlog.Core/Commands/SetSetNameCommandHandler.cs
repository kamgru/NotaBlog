using NotaBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Core.Commands
{
    public class SetSetNameCommandHandler : ICommandHandler<SetSeName>
    {
        private readonly IStoryRepository _storyRepository;

        public SetSetNameCommandHandler(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public async Task<CommandValidationResult> Handle(SetSeName command)
        {
            var story = await _storyRepository.Get(command.EntityId);
            if (story == null)
            {
                return new CommandValidationResult
                {
                    Errors = new[] { "Story not found" }
                };
            }

            if (string.IsNullOrEmpty(command.SeName))
            {
                return new CommandValidationResult
                {
                    Errors = new[] { "Search engine name must not be empty" }
                };
            }

            story.SetSeName(command.SeName);

            await _storyRepository.Update(story);

            return new CommandValidationResult();
        }
    }
}

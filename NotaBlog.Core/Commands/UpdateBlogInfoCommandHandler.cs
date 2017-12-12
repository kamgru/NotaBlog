using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Core.Commands
{
    public class UpdateBlogInfoCommandHandler : ICommandHandler<UpdateBlogInfo>
    {
        private readonly ISettingsRepository _settingsRepository;

        public UpdateBlogInfoCommandHandler(ISettingsRepository settingsRepository)
            => _settingsRepository = settingsRepository;

        public async Task<CommandValidationResult> Handle(UpdateBlogInfo command)
        {
            var blogInfo = new BlogInfo
            {
                Title = command.Title,
                Description = command.Description
            };

            await _settingsRepository.UpdateBlogInfo(blogInfo);

            return new CommandValidationResult();
        }
    }
}

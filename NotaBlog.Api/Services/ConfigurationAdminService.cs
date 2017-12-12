using NotaBlog.Api.Dto;
using NotaBlog.Core.Commands;
using NotaBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Api.Services
{
    public class ConfigurationAdminService
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly ICommandDispatcher _commandDispatcher;

        public ConfigurationAdminService(ISettingsRepository settingsRepository, ICommandDispatcher commandDispatcher)
        {
            _settingsRepository = settingsRepository;
            _commandDispatcher = commandDispatcher;
        }

        public async Task<Result> UpdateBlogInfo(string title, string description)
        {
            var submitResult = await _commandDispatcher.Submit(new UpdateBlogInfo
            {
                Title = title,
                Description = description
            });

            return new Result
            {
                Success = submitResult.Success
            };
        }
    }
}

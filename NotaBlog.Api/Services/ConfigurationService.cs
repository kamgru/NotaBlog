using NotaBlog.Api.Dto;
using NotaBlog.Core.Commands;
using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Api.Services
{
    public class ConfigurationService
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly ICommandDispatcher _commandDispatcher;

        public ConfigurationService(ISettingsRepository settingsRepository, ICommandDispatcher commandDispatcher)
        {
            _settingsRepository = settingsRepository;
            _commandDispatcher = commandDispatcher;
        }

        public Task<BlogInfo> GetBlogInfo() 
            => _settingsRepository.GetBlogInfo();

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

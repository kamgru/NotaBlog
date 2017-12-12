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

        public ConfigurationService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public Task<BlogInfo> GetBlogInfo() 
            => _settingsRepository.GetBlogInfo();
    }
}

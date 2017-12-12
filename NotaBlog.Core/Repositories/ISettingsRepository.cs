using NotaBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Core.Repositories
{
    public interface ISettingsRepository
    {
        Task<BlogInfo> GetBlogInfo();
        Task UpdateBlogInfo(BlogInfo blogInformation);
    }
}

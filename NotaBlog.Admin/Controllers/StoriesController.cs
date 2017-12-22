using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotaBlog.Api.Services;

namespace NotaBlog.Admin.Controllers
{
    [Route("api/stories")]
    public class StoriesController : Controller
    {
        private readonly StoryAdminService _storyAdminService;

        public StoriesController(StoryAdminService storyAdminService)
        {
            _storyAdminService = storyAdminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStoryHeaders(int page, int count)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> GetStory(string id)
        {
            throw new NotImplementedException();
        }
    }
}
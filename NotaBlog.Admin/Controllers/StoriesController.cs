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
            return Ok(await _storyAdminService.GetStoryHeaders(page, count));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetStory(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var story = await _storyAdminService.GetStory(guid);
                if (story == null)
                {
                    return NotFound();
                }

                return Ok(story);
            }

            return BadRequest();
        }
    }
}
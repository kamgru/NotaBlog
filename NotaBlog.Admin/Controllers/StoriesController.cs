using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotaBlog.Admin.Models;
using NotaBlog.Api.Dto;
using NotaBlog.Api.Services;

namespace NotaBlog.Admin.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpPost]
        public async Task<IActionResult> CreateStory()
        {
            var result = await _storyAdminService.CreateStory();
            if (result.Success)
            {
                return Ok(result.StoryId);
            }

            return BadRequest();
        }

        [Route("{id}/content")]
        [HttpPatch]
        public async Task<IActionResult> UpdateStoryContent(string id, [FromBody]UpdateContentModel model)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var result = await _storyAdminService.UpdateStory(new UpdateStoryRequest
                {
                    StoryId = guid,
                    Title = model.Title,
                    Content = model.Content
                });

                return result.Success
                    ? (IActionResult)Ok()
                    : BadRequest(result.ErrorMessage);
            }

            return NotFound();
        }

        [Route("{id}/publication-status")]
        [HttpPatch]
        public async Task<IActionResult> UpdateStoryPublicationStatus(string id, [FromBody]UpdateStatusModel model)
        {
            if (Guid.TryParse(id, out var guid))
            {
                if (model.StoryStatus == StoryStatus.Published)
                {
                    var result = await _storyAdminService.PublishStory(guid);
                    return result.Success
                        ? (IActionResult)Ok()
                        : BadRequest(result.ErrorMessage);
                }

                if (model.StoryStatus == StoryStatus.Unpublished)
                {
                    var result = await _storyAdminService.UnpublishStory(guid);
                    return result.Success
                        ? (IActionResult)Ok()
                        : BadRequest(result.ErrorMessage);
                }
            }

            return NotFound();
        }
    }
}
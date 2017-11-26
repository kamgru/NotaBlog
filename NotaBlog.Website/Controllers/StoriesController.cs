using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotaBlog.Api;

namespace NotaBlog.Website.Controllers
{
    public class StoriesController : Controller
    {
        private readonly StoryService _storyService;

        public StoriesController(StoryService storyService)
        {
            _storyService = storyService;
        }

        public async Task<IActionResult> Index()
        {
            var stories = await _storyService.GetLatestStories(5);
            return View(stories);
        }
    }
}
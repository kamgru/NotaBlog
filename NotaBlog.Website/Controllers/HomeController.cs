using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotaBlog.Api;
using NotaBlog.Website.Models;
using NotaBlog.Api.Services;

namespace NotaBlog.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoryService _storyService;
        private readonly ConfigurationService _configurationService;

        public HomeController(StoryService storyService, ConfigurationService configurationService)
        {
            _storyService = storyService;
            _configurationService = configurationService;
        }

        public async Task<IActionResult> Index()
        {
            var blogInfo = await _configurationService.GetBlogInfo();
            ViewBag.BlogInfo = blogInfo;

            var stories = await _storyService.GetLatestLeads(5);
            return View(stories);
        }

        public async Task<IActionResult> Story(string title)
        {
            var story = await _storyService.GetPublishedStory(title);
            return story != null
                ? View(story)
                : (IActionResult)RedirectToAction("PageNotFound", "Error");
        }
    }
}

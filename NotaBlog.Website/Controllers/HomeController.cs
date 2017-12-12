using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotaBlog.Api;
using NotaBlog.Website.Models;

namespace NotaBlog.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoryService _storyService;

        public HomeController(StoryService storyService)
        {
            _storyService = storyService;
        }

        public async Task<IActionResult> Index()
        {
            var stories = await _storyService.GetLatestLeads(5);
            return View(stories);
        }
    }
}

using NotaBlog.Api.ViewModels;
using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotaBlog.Api.Services
{
    public class StoryService
    {
        private readonly IStoryRepository _storyRepository;

        public StoryService(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public async Task<BlogStoryViewModel> GetStory(Guid id)
        {
            var story = await _storyRepository.Get(id);
            if (story == null)
            {
                return null;
            }

            return new BlogStoryViewModel
            {
                Id = story.Id,
                Title = story.Title,
                Content = story.Content,
            };
        }

        public async Task<BlogStoryViewModel> GetPublishedStory(string seName)
        {
            var story = await _storyRepository.Get(seName);
            if (story == null || story.PublicationStatus != PublicationStatus.Published)
            {
                return null;
            }

            return new BlogStoryViewModel
            {
                Id = story.Id,
                Published = story.Published.Value,
                Title = story.Title,
                Content = story.Content
            };
        }

        public async Task<IEnumerable<BlogStoryViewModel>> GetLatestStories(int count)
        {
            var stories = await _storyRepository.Get(StoryFilter.LastestStories(count));

            return stories.Items.Select(item => new BlogStoryViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Content = item.Content,
                Published = item.Published.Value,
            });
        }

        public async Task<IEnumerable<StoryLeadViewModel>> GetLatestLeads(int count)
        {
            var stories = await _storyRepository.Get(StoryFilter.LastestStories(count));

            return stories.Items.Select(item => new StoryLeadViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Published = item.Published.Value,
                LeadContent = item.GetLeadParagraph(),
                SeName = item.SeName
            });
        }
    }
}

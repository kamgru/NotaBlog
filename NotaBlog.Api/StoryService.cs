using NotaBlog.Core.Entities;
using NotaBlog.Core.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotaBlog.Api
{
    public class StoryService
    {
        private readonly IStoryRepository _storyRepository;

        public StoryService(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public async Task<StoryViewModel> GetStory(Guid id)
        {
            var story = await _storyRepository.Get(id);
            if (story == null)
            {
                return null;
            }

            return new StoryViewModel
            {
                Id = story.Id,
                Title = story.Title,
                Content = story.Content,
                PublicationStatus = story.PublicationStatus,
                Created = story.Created
            };
        }

        public async Task<IEnumerable<StoryViewModel>> GetLatestStories(int count)
        {
            var stories = await _storyRepository.Get(StoryFilter.LastestStories(count));

            return stories.Items.Select(item => new StoryViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Content = item.Content,
                Published = item.Published.Value,
                Created = item.Created,
                PublicationStatus = item.PublicationStatus
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

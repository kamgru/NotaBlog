using NotaBlog.Core.Commands;
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
        private readonly CreateStoryHandler _createStoryHandler;
        private readonly PublishStoryHandler _publishStoryHandler;
        private readonly IStoryRepository _storyRepository;

        public StoryService(CreateStoryHandler createStoryHandler, PublishStoryHandler publishStoryHandler,
            IStoryRepository storyRepository)
        {
            _createStoryHandler = createStoryHandler;
            _publishStoryHandler = publishStoryHandler;
            _storyRepository = storyRepository;
        }

        public async Task<CreateStoryResult> CreateStory(string title, string content)
        {
            var command = new CreateStory
            {
                EntityId = Guid.NewGuid(),
                Title = title,
                Content = content
            };

            var validationResult = await _createStoryHandler.Handle(command);

            return new CreateStoryResult
            {
                Success = validationResult.Success,
                StoryId = command.EntityId
            };
        }

        public async Task<PublishStoryResult> PublishStory(Guid storyId)
        {
            var validationResult = await _publishStoryHandler.Handle(new PublishStory
            {
                EntityId = storyId
            });

            return new PublishStoryResult
            {
                StoryId = storyId,
                Success = validationResult.Success
            };
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
            var filter = new StoryFilter
            {
                Page = 1,
                Count = count,
                Predicate = story => story.PublicationStatus == PublicationStatus.Published,
                SortBy = story => story.Published,
                DescendingOrder = true
            };

            var stories = await _storyRepository.Get(filter);

            return stories.Items.Select(item => new StoryViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Content = item.Content,
                Published = item.Published.Value
            });
        }
    }
}

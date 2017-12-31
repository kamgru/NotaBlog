using NotaBlog.Api.Dto;
using NotaBlog.Api.ViewModels;
using NotaBlog.Core.Commands;
using NotaBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotaBlog.Api.Services
{
    public class StoryAdminService
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IStoryRepository _storyRepository;

        public StoryAdminService(ICommandDispatcher commandDispatcher, IStoryRepository storyRepository)
        {
            _commandDispatcher = commandDispatcher;
            _storyRepository = storyRepository;
        }

        public async Task<CreateStoryResult> CreateStory()
        {
            var command = new CreateStory
            {
                EntityId = Guid.NewGuid(),
            };

            var validationResult = await _commandDispatcher.Submit(command);

            return new CreateStoryResult
            {
                Success = validationResult.Success,
                StoryId = command.EntityId
            };
        }

        public async Task<PublishStoryResult> PublishStory(Guid storyId)
        {
            var validationResult = await _commandDispatcher.Submit(new PublishStory
            {
                EntityId = storyId
            });

            return new PublishStoryResult
            {
                StoryId = storyId,
                Success = validationResult.Success,
                Errors = validationResult.Errors
            };
        }

        public async Task<PaginatedData<StoryHeaderViewModel>> GetStoryHeaders(int page, int count)
        {
            if (page < 1 || count < 1)
            {
                return new PaginatedData<StoryHeaderViewModel>
                {
                    Items = new List<StoryHeaderViewModel>()
                };
            }

            var filter = new StoryFilter
            {
                Page = page,
                Count = count,
                DescendingOrder = true,
                SortBy = x => x.Created
            };

            var stories = await _storyRepository.Get(filter);

            return new PaginatedData<StoryHeaderViewModel>
            {
                Items = stories.Items.Select(story => new StoryHeaderViewModel
                {
                    Id = story.Id,
                    Created = story.Created,
                    PublicationStatus = story.PublicationStatus,
                    Published = story.Published,
                    Title = story.Title
                }),
                TotalCount = stories.TotalCount
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
                Content = story.Content,
                Created = story.Created,
                Id = story.Id,
                PublicationStatus = story.PublicationStatus,
                Published = story.Published,
                SeName = story.SeName,
                Title = story.Title,
                Updated = story.Updated
            };
        }

        public async Task<Result> UpdateStory(UpdateStoryRequest request)
        {
            var validationResult = await _commandDispatcher.Submit(new UpdateStory
            {
                EntityId = request.StoryId,
                Title = request.Title,
                Content = request.Content
            });

            return new Result
            {
                Success = validationResult.Success,
                Errors = validationResult.Errors
            };
        }
    }
}

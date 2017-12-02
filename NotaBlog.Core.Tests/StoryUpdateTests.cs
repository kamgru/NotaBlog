using FluentAssertions;
using NotaBlog.Core.Entities;
using NotaBlog.Tests.Common.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class StoryUpdateTests
    {
        [Fact]
        public void WhenUpdating_ItShouldSetTitleAndContent()
        {
            var story = Story.CreateNew(Guid.NewGuid(), new MockDateTimeProvider());

            story.Update("new title", "new content", new MockDateTimeProvider());

            story.Title.ShouldBeEquivalentTo("new title");
            story.Content.ShouldAllBeEquivalentTo("new content");
        }

        [Fact]
        public void WhenUpdating_ItShouldSetUpdateDateToNow()
        {
            var dateTimeProvider = new MockDateTimeProvider
            {
                DateTimeNow = DateTime.Parse("2016-12-16 12:35:00")
            };

            var story = Story.CreateNew(Guid.NewGuid(), dateTimeProvider);

            story.Update("new title", "new content", dateTimeProvider);

            story.Updated.Value.ShouldBeEquivalentTo(dateTimeProvider.Now());
        }
    }
}

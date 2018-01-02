using FluentAssertions;
using NotaBlog.Core.Entities;
using NotaBlog.Tests.Common.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class StoryPublishTests : StoryTestsBase
    {
        [Fact]
        public void WhenPublished_ItShouldSetPublicationStatusToPublished()
        {
            var story = CreateDefault();

            story.Publish(new MockDateTimeProvider());

            story.PublicationStatus.ShouldBeEquivalentTo(PublicationStatus.Published);
        }

        [Fact]
        public void WhenPublishedDateNull_ItShouldSetPublishedDateToNow()
        {
            var dateTimeProvider = new MockDateTimeProvider
            {
                DateTimeNow = DateTime.Parse("2016-12-16 12:35:00")
            };

            var story = CreateDefault();

            story.Publish(dateTimeProvider);

            story.Published.Should().BeCloseTo(dateTimeProvider.DateTimeNow);
        }

        [Fact]
        public void GivenPreviouslyPublishedStory_WhenPublishedDateNotNull_ItShouldNotUpdatePublishedDate()
        {
            var firstPublished = DateTime.Parse("2016-12-16 12:35:00");
            var dateTimeProvider = new MockDateTimeProvider { DateTimeNow = firstPublished };

            var story = CreateDefault();
            story.Publish(dateTimeProvider);
            story.Unpublish();

            dateTimeProvider.DateTimeNow = firstPublished.AddMonths(2);
            story.Publish(dateTimeProvider);

            story.Published.ShouldBeEquivalentTo(firstPublished);
        }
    }
}

using FluentAssertions;
using NotaBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class StoryUnpublishTests : StoryTestsBase
    {
        [Fact]
        public void WhenUnpublishing_ItShouldSetStatusToDraft()
        {
            var story = CreatePublished();

            story.Unpublish();

            story.PublicationStatus.ShouldBeEquivalentTo(PublicationStatus.Draft);
        }
    }
}

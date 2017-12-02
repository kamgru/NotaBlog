using FluentAssertions;
using NotaBlog.Core.Entities;
using NotaBlog.Tests.Common.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class StorySetSeNameTests
    {
        [Fact]
        public void ItShouldSetSeName()
        {
            var story = Story.CreateNew(Guid.NewGuid(), new MockDateTimeProvider());
            var expected = "se-name-test-url";

            story.SetSeName(expected);

            story.SeName.ShouldBeEquivalentTo(expected);
        }
    }
}

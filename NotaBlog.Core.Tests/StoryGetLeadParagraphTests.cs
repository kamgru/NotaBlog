using FluentAssertions;
using NotaBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NotaBlog.Core.Tests
{
    public class StoryGetLeadParagraphTests : StoryTestsBase
    {
        [Fact]
        public void WhenContentEmpty_ItShouldReturnEmpty()
        {
            var story = CreateDefault();

            var result = story.GetLeadParagraph();

            result.Should().BeEmpty();
        }

        [Theory]
        [MemberData(nameof(StoryHtmlContent))]
        public void WhenContentNotEmpty_ItShouldReturnTextInFirstHtmlParagraph(string content, string lead)
        {
            var story = CreateWithContent(content);

            var result = story.GetLeadParagraph();

            result.ShouldBeEquivalentTo(lead);
        }

        public static IEnumerable<object> StoryHtmlContent()
        {
            yield return new object[]
            {
                "<p>test</p>", "test"
            };

            yield return new object[]
            {
                "<h2>sub title</h2><p>lead paragraph</p><p>other content</p>", "lead paragraph"
            };

            yield return new object[]
            {
                "</p>", ""
            };

            yield return new object[]
            {
                "<p>....", ""
            };

            yield return new object[]
            {
                "</p>...<p>", ""
            };
        }

        private Story CreateWithContent(string content)
        {
            var story = CreateDefault();
            story.Update("title", content, _dateTimeProvider);
            return story;
        }
    }
}

using NotaBlog.Core.Entities;
using System;
using System.Linq.Expressions;

namespace NotaBlog.Core.Repositories
{
    public class StoryFilter
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public Expression<Func<Story, bool>> Predicate { get; set; }
        public Expression<Func<Story, object>> SortBy { get; set; }
        public bool DescendingOrder { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Repositories
{
    public class PaginatedResult<TEntity>
    {
        public long TotalCount { get; set; }
        public IEnumerable<TEntity> Items { get; set; }
    }
}

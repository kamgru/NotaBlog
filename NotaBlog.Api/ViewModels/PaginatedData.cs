using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Api.ViewModels
{
    public class PaginatedData<T>
    {
        public long TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}

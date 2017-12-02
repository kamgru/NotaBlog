using NotaBlog.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Tests.Common.Mocks
{
    public class MockDateTimeProvider : IDateTimeProvider
    {
        public DateTime DateTimeNow { get; set; } = DateTime.Now;

        public DateTime Now()
        {
            return DateTimeNow;
        }
    }
}

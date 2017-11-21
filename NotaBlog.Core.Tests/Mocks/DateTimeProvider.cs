using NotaBlog.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Tests.Mocks
{
    class DateTimeProvider : IDateTimeProvider
    {
        public DateTime DateTimeNow { get; set; }

        public DateTime Now()
        {
            return DateTimeNow;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NotaBlog.Core.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}

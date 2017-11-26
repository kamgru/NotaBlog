﻿using NotaBlog.Core.Commands;
using NotaBlog.Core.Repositories;
using NotaBlog.Core.Services;
using NotaBlog.Core.Tests.Mocks;

namespace NotaBlog.Core.Tests
{
    public class CreateStoryCommandTestsBase
    {
        protected CreateStoryHandler Handler(IStoryRepository repository = null, IDateTimeProvider dateTimeProvider = null)
        {
            repository = repository ?? new InMemoryStoryRepository();
            dateTimeProvider = dateTimeProvider ?? new Mocks.DateTimeProvider();
            return new CreateStoryHandler(repository, dateTimeProvider);
        }
    }
}

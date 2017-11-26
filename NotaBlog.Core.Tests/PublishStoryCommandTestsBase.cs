using NotaBlog.Core.Commands;
using NotaBlog.Core.Repositories;
using NotaBlog.Core.Services;
using NotaBlog.Core.Tests.Mocks;

namespace NotaBlog.Core.Tests
{
    public class PublishStoryCommandTestsBase
    {
        protected PublishStoryHandler Handler(IStoryRepository repository = null, IDateTimeProvider dateTimeProvider = null)
        {
            repository = repository ?? new InMemoryStoryRepository();
            dateTimeProvider = dateTimeProvider ?? new DateTimeProvider();
            return new PublishStoryHandler(repository, dateTimeProvider);
        }
    }
}

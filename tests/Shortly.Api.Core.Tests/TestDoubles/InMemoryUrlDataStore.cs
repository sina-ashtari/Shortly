using Shortly.Core.Url;
using Shortly.Core.Url.Add;

namespace Shortly.Api.Core.Tests.TestDoubles;

public class InMemoryUrlDataStore : Dictionary<string, ShortenedUrl>, IUrlDataStore
{
    public Task AddAsync(ShortenedUrl shortened, CancellationToken token)
    {
        Add(shortened.ShortUrl, shortened);
        return Task.CompletedTask;
    }
}
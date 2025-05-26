using Microsoft.Extensions.Time.Testing;
using Shortly.Api.Core.Tests.TestDoubles;
using Shortly.Core;
using Shortly.Core.Url.Add;

namespace Shortly.Api.Core.Tests.Urls;

public class AddUrlScenarios
{
    private readonly AddUrlHandler _urlHandler;
    private readonly InMemoryUrlDataStore _urlDataStore;
    private readonly FakeTimeProvider _timeProvider;

    public AddUrlScenarios(InMemoryUrlDataStore urlDataStore)
    {
        _urlDataStore = urlDataStore;
        var tokenProvider = new TokenProvider();
        tokenProvider.AssignRange(1, 5);
        var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);
        _timeProvider = new FakeTimeProvider();
        _urlHandler = new AddUrlHandler(shortUrlGenerator, _urlDataStore, _timeProvider);
    }

    [Fact]
    public async Task Should_return_shortened_url()
    {
        var request = CreateAddUrlRequest();
        var response = await _urlHandler.HandleAsync(request, default);
        response.ShortUrl.Should().NotBeEmpty();
        response.ShortUrl.Should().Be("1");
    }

    [Fact]
    public async Task Should_save_short_url()
    {
        var request = CreateAddUrlRequest();
        var response = await _urlHandler.HandleAsync(request, default);
        _urlDataStore.Should().ContainKey(response.ShortUrl);
    }
    
    [Fact]
    public async Task Should_save_short_url_with_created_by_and_created_on()
    {
        var request = CreateAddUrlRequest();
        var response = await _urlHandler.HandleAsync(request, default);
        _urlDataStore.Should().ContainKey(response.ShortUrl);
        _urlDataStore[response.ShortUrl].CreatedBy.Should().Be(request.CreatedBy);
        _urlDataStore[response.ShortUrl].CreatedOn.Should().Be(_timeProvider.GetUtcNow());
    }

    private static AddUrlRequest CreateAddUrlRequest()
    {
        return new AddUrlRequest(new Uri("localhost"), "user@user");
    }
}


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
        response.Value!.ShortUrl.Should().NotBeEmpty();
        response.Value!.ShortUrl.Should().Be("1");
    }

    [Fact]
    public async Task Should_save_short_url()
    {
        var request = CreateAddUrlRequest();
        var response = await _urlHandler.HandleAsync(request, default);
        response.Succeeded.Should().BeTrue();   
        _urlDataStore.Should().ContainKey(response.Value!.ShortUrl);
    }
    
    [Fact]
    public async Task Should_save_short_url_with_created_by_and_created_on()
    {
        var request = CreateAddUrlRequest();
        var response = await _urlHandler.HandleAsync(request, default);
        response.Succeeded.Should().BeTrue();   
        _urlDataStore.Should().ContainKey(response.Value!.ShortUrl);
        _urlDataStore[response.Value!.ShortUrl].CreatedBy.Should().Be(request.CreatedBy);
        _urlDataStore[response.Value!.ShortUrl].CreatedOn.Should().Be(_timeProvider.GetUtcNow());
    }

    [Fact]
    public async Task Should_return_error_if_createdBy_is_empty()
    {
        var request = CreateAddUrlRequest();
        var response = await _urlHandler.HandleAsync(request, default);
        response.Succeeded.Should().BeFalse();
        response.Error.Code.Should().Be("MISSING");
    }
    

    private static AddUrlRequest CreateAddUrlRequest(string createdBy = "user@user")
    {
        return new AddUrlRequest(new Uri("localhost"), createdBy);
    }
}


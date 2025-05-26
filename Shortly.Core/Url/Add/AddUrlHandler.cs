namespace Shortly.Core.Url.Add;

public class AddUrlHandler
{
    private readonly ShortUrlGenerator _shortUrlGenerator;
    private readonly IUrlDataStore _urlDataStore;
    private readonly TimeProvider _timeProvider;

    public AddUrlHandler(ShortUrlGenerator shortUrlGenerator, IUrlDataStore urlDataStore, TimeProvider timeProvider)
    {
        _shortUrlGenerator = shortUrlGenerator;
        _urlDataStore = urlDataStore;
        _timeProvider = timeProvider;
    }

    public async Task<AddUrlResponse> HandleAsync(AddUrlRequest request, CancellationToken token)
    {
        var shortened = new ShortenedUrl(request.LongUrl, _shortUrlGenerator.GenerateUniqueUrl(), request.CreatedBy, _timeProvider.GetUtcNow());
        await _urlDataStore.AddAsync(shortened, token);
        return new AddUrlResponse(request.LongUrl, shortened.ShortUrl);
    }
}
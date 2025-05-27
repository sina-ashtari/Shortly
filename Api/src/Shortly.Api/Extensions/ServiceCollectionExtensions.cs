using Shortly.Core;
using Shortly.Core.Url;
using Shortly.Core.Url.Add;

namespace Shortly.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUrlFeature(this IServiceCollection services)
    {
        services.AddScoped<AddUrlHandler>();
        services.AddSingleton<TokenProvider>(_ =>
        {
            var tokenProvider = new TokenProvider();
            tokenProvider.AssignRange(1, 1000);
            return tokenProvider;
        });
        services.AddScoped<ShortUrlGenerator>();
        services.AddScoped<IUrlDataStore, InMemoryUrlDataStore>();
        return services;
    }
    
}
public class InMemoryUrlDataStore: Dictionary<string, ShortenedUrl>, IUrlDataStore
{
    public Task AddAsync(ShortenedUrl shortened, CancellationToken token)
    {
        Add(shortened.ShortUrl, shortened);
        return Task.CompletedTask;
    }
}
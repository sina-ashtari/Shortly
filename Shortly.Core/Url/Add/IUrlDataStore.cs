namespace Shortly.Core.Url.Add;

public interface IUrlDataStore
{
    Task AddAsync(ShortenedUrl shortened, CancellationToken token);
}
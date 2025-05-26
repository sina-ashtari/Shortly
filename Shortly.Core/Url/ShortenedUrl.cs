namespace Shortly.Core.Url;

public class ShortenedUrl
{
    public ShortenedUrl(Uri longUrl, string shortUrl, string CreatedBy, DateTimeOffset CreatedOn)
    {
        LongUrl = longUrl;
        ShortUrl = shortUrl;
        this.CreatedOn = CreatedOn;
        this.CreatedBy = CreatedBy;
    }

    public Uri LongUrl { get; set; }
    public string ShortUrl { get; set; }
    public string CreatedBy { get;  }
    public DateTimeOffset CreatedOn{ get;  }
}
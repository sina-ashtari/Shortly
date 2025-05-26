namespace Shortly.Core.Url.Add;

public class ShortUrlGenerator(TokenProvider tokenProvider)
{
    public string GenerateUniqueUrl()
    {
        return tokenProvider.GetToken().EncodeToBase62();
    }
}
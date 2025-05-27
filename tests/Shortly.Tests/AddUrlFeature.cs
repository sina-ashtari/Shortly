using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Shortly.Core.Url.Add;

namespace Shortly.Tests;

public class AddUrlFeature : IClassFixture<ApiFixture>
{
    private readonly HttpClient _client;

    public AddUrlFeature(ApiFixture fixture)
    {
        _client = fixture.CreateClient();
    }

    [Fact] 
    public async Task Given_Long_Url_Should_Return_Shortened_Url()
    {
        var response = await _client.PostAsJsonAsync<AddUrlRequest>("/api/urls", new AddUrlRequest(new Uri("https://localhost"),""));
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var addUrlResponse = await response.Content.ReadFromJsonAsync<AddUrlResponse>();
        
        addUrlResponse!.ShortUrl.Should().NotBeNull();
    }
}
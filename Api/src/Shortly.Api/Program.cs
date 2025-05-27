using Shortly.Api.Extensions;
using Shortly.Core.Url.Add;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddUrlFeature();
// Fake Time Provider Injection
builder.Services.AddSingleton(TimeProvider.System);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/api/urls", async (
    AddUrlHandler handler,
    AddUrlRequest request,
    CancellationToken token
) =>
{
    var requestWithUser = request with
    {
        CreatedBy = "localhost"
    };
    
    var result = await handler.HandleAsync(requestWithUser, token);

    return !result.Succeeded ? Results.BadRequest(result.Error) : Results.Created($"/api/urls/{result.Value!.ShortUrl}", result.Value);
});

app.Run();

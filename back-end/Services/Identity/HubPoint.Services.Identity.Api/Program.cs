using HubPoint.Services.Common.Contracts.Requests;
using HubPoint.Services.Identity.Api;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(cfg =>
{
    cfg.UsingRabbitMq((ctx, rbt) =>
    {
        rbt.Host("172.29.14.167", "/", h =>
        {
            h.Username("hubpoint");
            h.Password("hubpoint.2023");
        });
        
        rbt.ConfigureEndpoints(ctx);
    });
});

builder.Services.AddSingleton<TokenService>();

var app = builder.Build();

app.MapPost("/token/generate",  async ([FromBody] GenerateTokenRequest request, TokenService service, IRequestClient<GetUser> client, ILogger<Program> logger, CancellationToken ct) =>
{
    logger.LogInformation("Requested {UserName}", request.UserName);
    var response = await client.GetResponse<GetUserResult>(new { request.UserName }, ct);
    return response.Message.UserName.Equals(request.UserName) ? service.GenerateToken() : "";
});

app.MapGet("/", () => NewId.Next().ToString());

app.Run();
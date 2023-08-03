using EasyNetQ;
using HubPoint.Services.Common.Infrastructure.Events;
using HubPoint.Services.Security.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TokenService>();

builder.Services.RegisterEasyNetQ(builder.Configuration.GetConnectionString("RabbitMQ"), s => s.EnableSystemTextJson());

var app = builder.Build();

app.MapPost("/token/generate",  ([FromBody] GenerateTokenRequest request, TokenService service) => service.GenerateToken());

app.AddSubscribers(typeof(UserCreated));

app.Run();


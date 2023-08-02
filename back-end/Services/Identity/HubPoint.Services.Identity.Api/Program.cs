using EasyNetQ;
using HubPoint.Services.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TokenService>();

var app = builder.Build();

app.MapPost("/token/generate",  ([FromBody] GenerateTokenRequest request, TokenService service) => service.GenerateToken());

var bus = RabbitHutch.CreateBus("host=localhost;virtualHost=hub-point;username=hub-point;password=hub-point", s => s.EnableSystemTextJson());
bus.PubSub.SubscribeAsync<UserCreated>("user-created", msg => Console.WriteLine(msg.UserId));

app.Run();


using HubPoint.Services.Common.Abstractions.Commands;
using HubPoint.Services.Common.Infrastructure;
using HubPoint.Services.Common.Infrastructure.Dispatchers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TokenService>();

builder.Services.AddRabbitMq();

builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();

var app = builder.Build();

app.MapPost("/token/generate",  ([FromBody] GenerateTokenRequest request, TokenService service) => service.GenerateToken());

app.MapPost("/", async ([FromBody] TestCommand command, ICommandDispatcher dispatcher, ILogger<Program> logger) =>
{
    logger.LogInformation("{Message}", command.Message);
    await dispatcher.Send(command);
});

app.Run();
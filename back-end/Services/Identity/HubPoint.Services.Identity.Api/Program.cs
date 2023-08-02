var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TokenService>();

var app = builder.Build();

app.MapPost("/token/generate",  ([FromBody] GenerateTokenRequest request, TokenService service) => service.GenerateToken());

app.Run();
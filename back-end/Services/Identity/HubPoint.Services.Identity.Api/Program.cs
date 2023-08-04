using HubPoint.Services.Common.Infrastructure;
using HubPoint.Services.Common.Infrastructure.Events;
using HubPoint.Services.Identity.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TokenService>();

builder.Services.AddDbContext<IdentityDbContext>(opts => opts.UseInMemoryDatabase("hub-point-2"));

builder.Services.AddInfrastructure(c =>
{
    c.RabbitMqConnectionString = builder.Configuration.GetConnectionString("RabbitMQ");
    c.AddPostProcessor(typeof(CommitProcessor<,>));
});

var app = builder.Build();

app.MapPost("/token/generate",  ([FromBody] GenerateTokenRequest request, [FromServices] TokenService service) => service.GenerateToken());

app.MapGet("/integration/users", async ([FromServices] IdentityDbContext context) => await context.Users.ToListAsync());

app.UseIntegrationEvents("idt");

app.Run();


using HubPoint.Services.Common.Infrastructure.Jwt;
using HubPoint.Services.Security.Api;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(cfg =>
{
    cfg.AddConsumer<GetUserConsumer>();
    
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

builder.Services.AddControllers();

builder.Services.AddHealthChecks();

builder.Services.AddJwtAuthentication();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseHealthChecks("/health");

app.MapControllers();

app.Run();
using HubPoint.Services.Common.Abstractions.Commands;
using HubPoint.Services.Common.Infrastructure.Dispatchers;
using HubPoint.Services.Common.Infrastructure.Jwt;
using HubPoint.Services.Security.Api.Application;
using HubPoint.Services.Security.Api.Domain.Events;
using HubPoint.Services.Security.Api.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

builder.Services.AddMassTransit(cfg =>
{
    
    cfg.AddMediator(opts =>
    {
        opts.AddConsumer<CreateUserCommandHandler>();
        opts.AddConsumer<UserCreatedHandler>();
    });

    cfg.UsingRabbitMq((ctx, rbt) =>
    {
        rbt.Host(builder.Configuration["RabbitMQ:Host"], "/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"]);
            h.Password(builder.Configuration["RabbitMQ:Password"]);
        });

        rbt.ConfigureEndpoints(ctx);
    });
});

builder.Services.AddDbContext<AppDbContext>(opts => opts.UseInMemoryDatabase("hubpoint"));

// builder.Services.AddDbContext<AppDbContext>(opts => opts.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers();

builder.Services.AddHealthChecks();

builder.Services.AddJwtAuthentication();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseHealthChecks("/health");

app.MapControllers();

app.Run();
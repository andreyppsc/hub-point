using HubPoint.Services.Common.Abstractions.Events;
using HubPoint.Services.Common.Infrastructure.Events;
using HubPoint.Services.Common.Infrastructure.Jwt;
using HubPoint.Services.Security.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IOutbox, InMemoryOutbox>();

builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssemblyContaining<Program>();
    c.AddOpenRequestPostProcessor(typeof(CommitPostProcessor<,>));
    c.AddOpenRequestPostProcessor(typeof(OutboxProcessor<,>));
    c.NotificationPublisherType = typeof(IntegrationEventsPublisher);
});

builder.Services.AddDbContext<AppDbContext>(opts => opts.UseInMemoryDatabase("hub-point"));

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
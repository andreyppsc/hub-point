using HubPoint.Services.Common.Infrastructure;
using HubPoint.Services.Security.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SecurityDbContext>(opts => opts.UseInMemoryDatabase("hub-point"));

builder.Services.AddInfrastructure(c =>
{
    c.RabbitMqConnectionString = builder.Configuration.GetConnectionString("RabbitMQ");
    c.AddPostProcessor(typeof(CommitProcessor<,>));
});



// builder.Services.AddDbContext<AppDbContext>(opts => opts.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseHealthChecks("/health");

app.MapControllers();

app.Run();
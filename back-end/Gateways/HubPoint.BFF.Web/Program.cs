var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("routes.json", false, true);

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddJwtAuthentication();

var app = builder.Build();

await app.UseOcelot();

app.Run();
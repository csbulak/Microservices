using Ocelot.DependencyInjection;
using Ocelot.Middleware;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("configuration.development.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot(configuration);

var app = builder.Build();
app.UseOcelot();
app.Run();

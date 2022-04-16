using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Host.ConfigureAppConfiguration(
    config => config.AddJsonFile($"ocelot.{env}.json")
    );

builder.Services.AddOcelot();

var app = builder.Build();

app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.Run();

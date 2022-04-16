using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Host.ConfigureAppConfiguration(
    config => config.AddJsonFile($"ocelot.{env}.json")
    );

//Add Jwt Authentication

var key = "THIS IS PRIVATE SECRET KEY";

builder.Services.AddAuthentication(
    option =>
    {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddOcelot();

var app = builder.Build();

app.UseOcelot().Wait();

app.UseAuthentication();

app.MapGet("/", () => "Hello World!");

app.Run();

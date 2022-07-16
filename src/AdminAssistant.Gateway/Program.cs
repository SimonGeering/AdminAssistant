using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("ocelot.docker.json");
builder.Services.AddOcelot(); // https://ocelot.readthedocs.io/en/latest/index.html

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseOcelot().Wait(); // MUST BE LAST as dosen't call next middleware
app.Run();

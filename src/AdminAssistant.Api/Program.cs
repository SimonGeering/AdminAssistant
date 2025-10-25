using Ardalis.GuardClauses;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

const string WebAPIVersion = "v1";
const string WebAPITitle = $"Admin Assistant WebAPI {WebAPIVersion}.";

builder.Services.AddProblemDetails();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(c =>
{
    // See https://github.com/domaindrivendev/Swashbuckle.AspNetCore for an overview of options available here.
    // https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters - examples for getting swagger to do what you want

    // Skip documenting any controller without a Group name from the ApiExplorerSettings attribute ...
    c.DocInclusionPredicate((_, api) => string.IsNullOrWhiteSpace(api.GroupName) == false);
    c.TagActionsBy(api =>
    {
        // Group by Group name from the ApiExplorerSettings attribute ...
        Guard.Against.Null(api.GroupName, nameof(api.GroupName)); // Should be covered by DocInclusionPredicate.
        return new string[] { api.GroupName };
    });
    c.SwaggerDoc(WebAPIVersion, new OpenApiInfo { Title = WebAPITitle, Version = WebAPIVersion }); // Add OpenAPI/Swagger middleware

    // Include documentation from Annotations (Swashbuckle.AspNetCore.Annotations)...
    c.EnableAnnotations(); // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#install-and-enable-annotations
});

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // /openapi/v1.json
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", WebAPITitle);
        c.RoutePrefix = "api/docs";
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapDefaultEndpoints();

await app.RunAsync();

using Aspire.Hosting;
using k8s.Models;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AdminAssistant_Blazor_Server>("AdminAssistantBlazorServer");

await builder.Build().RunAsync();

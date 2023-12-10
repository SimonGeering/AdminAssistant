var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AdminAssistant_Blazor_Server>("AdminAssistant.Blazor.Server");

builder.Build().Run();

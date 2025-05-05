var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGet("/", () => "Hello World!");

app.Run();

// Hangfire postgress research
/*
https://docs.hangfire.io/en/latest/getting-started/aspnet-core-applications.html
https://github.com/hangfire-postgres/Hangfire.PostgreSql
https://dev.to/pradeepradyumna/your-first-hangfire-job-fornet8-with-postgresql-30nd
*/

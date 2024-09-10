var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver();

// https://www.milanjovanovic.tech/blog/implementing-an-api-gateway-for-microservices-with-yarp
// https://medium.com/@josephsims1/aspire-aspi8-deploy-microservices-effortlessly-with-cli-no-docker-or-yaml-needed-f30b58443107
// https://microsoft.github.io/reverse-proxy/articles/config-files.html

var app = builder.Build();
app.MapReverseProxy();
app.Run();

// Option for aspire
// https://github.com/rjygraham/AspireYarpTest

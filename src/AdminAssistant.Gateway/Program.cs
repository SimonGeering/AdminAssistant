using AdminAssistant;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddReverseProxy()
    .LoadFromMemory(
    [        
        new RouteConfig()
        {
            RouteId = "coreRoute",
            ClusterId = "coreCluster",
            Match = new RouteMatch { Path = "/core/{**catch-all}" },
            Transforms = [new Dictionary<string, string>() { { "PathRemovePrefix", "/core" } }]
        }
    ],
    [
        new ClusterConfig()
        {
            ClusterId = "coreCluster",
            Destinations = new Dictionary<string, DestinationConfig>()
            {
                { "coreDestination", new DestinationConfig { Address = $"http://{Constants.Services.CoreApi}", Health = $"http://{Constants.Services.CoreApi}/alive" } }
            }
        }
    ])
    //.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver();

// https://www.milanjovanovic.tech/blog/implementing-an-api-gateway-for-microservices-with-yarp
// https://medium.com/@josephsims1/aspire-aspi8-deploy-microservices-effortlessly-with-cli-no-docker-or-yaml-needed-f30b58443107
// https://microsoft.github.io/reverse-proxy/articles/config-files.html

var app = builder.Build();
app.MapReverseProxy();
app.Run();

// Option for aspire
// https://github.com/rjygraham/AspireYarpTest

// https://github.com/dotnet/aspire/issues/4605

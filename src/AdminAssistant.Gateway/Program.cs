using AdminAssistant;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddReverseProxy()
    .LoadFromMemory(
    [
        new RouteConfig()
        {
            RouteId = "fallbackRoute",
            ClusterId = "fallbackCluster",
            Match = new RouteMatch { Path = "/{**catch-all}" }
        },
        new RouteConfig()
        {
            RouteId = "coreRoute",
            ClusterId = "coreCluster",
            Match = new RouteMatch { Path = Constants.ApiGateway.CoreApiPrefix + "/{**catch-all}" },
            Transforms = [new Dictionary<string, string>() { { "PathRemovePrefix", Constants.ApiGateway.CoreApiPrefix } }]
        }
    ],
    [
        new ClusterConfig()
        {
            ClusterId = "fallbackCluster",
            Destinations = new Dictionary<string, DestinationConfig>()
            {
                { "fallbackDestination", new DestinationConfig { Address = $"http://{Constants.Services.Gateway}", Health = $"http://{Constants.Services.Gateway}/alive" } }
            }
        },
        new ClusterConfig()
        {
            ClusterId = "coreCluster",
            Destinations = new Dictionary<string, DestinationConfig>()
            {
                { "coreDestination", new DestinationConfig { Address = $"http://{Constants.Services.CoreApi}", Health = $"http://{Constants.Services.CoreApi}/alive" } }
            }
        }
    ])
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

// Yarp + swagger
// https://github.com/andreytreyt/yarp-swagger/tree/main/sample
// https://github.com/microsoft/reverse-proxy/issues/1789

//// Based on https://github.com/davidfowl/WaitForDependenciesAspire under MIT lic - thanks David Fowler :-)
//using Aspire.Hosting.ApplicationModel;
//using HealthChecks.Redis;

//namespace Aspire.Hosting;

//public static class RedisResourceHealthCheckExtensions
//{
//    /// <summary>
//    /// Adds a health check to the Redis server resource.
//    /// </summary>
//    public static IResourceBuilder<RedisResource> WithHealthCheck(this IResourceBuilder<RedisResource> builder)
//    {
//        return builder.WithAnnotation(HealthCheckAnnotation.Create(cs => new RedisHealthCheck(cs)));
//    }
//}

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0053:Use expression body for lambda expressions", Justification = "Readability.", Scope = "member", Target = "~M:AdminAssistant.Blazor.Server.Startup.ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection)")]
[assembly: SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "Broken HealthUI")]
[assembly: SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "Called once on startup", Scope = "member", Target = "~M:AdminAssistant.Blazor.Server.Startup.ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection)")]
[assembly: SuppressMessage("Minor Code Smell", "S3236:Caller information arguments should not be provided explicitly", Justification = "Specific situational message passed.", Scope = "member", Target = "~M:AdminAssistant.Blazor.Server.Startup.ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection)")]

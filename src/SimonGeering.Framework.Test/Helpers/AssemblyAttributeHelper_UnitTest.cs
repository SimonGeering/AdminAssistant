#pragma warning disable CA1707 // Identifiers should not contain underscores

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SimonGeering.Framework.Helpers;

namespace SimonGeering.Framework.Test.Helpers;

public class GetCulture
{
    [Fact]
    [Trait("Category", "Unit")]
    public void ThrowsException_GivenNullAssembly()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var act = () => services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetCulture(null!);

        // Assert ...
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(TestData.AssemblyParameterName);
    }
}

public class GetFullName
{
    [Fact]
    [Trait("Category", "Unit")]
    public void ReturnsTheAttributeValue_GivenAnAssemblyWithAVersionAttribute()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var result = services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetFullName(Assembly.GetExecutingAssembly());

        // Assert ...
        result.Should().Be(Assembly.GetExecutingAssembly().FullName);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void ThrowsException_GivenNullAssembly()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var act = () => services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetFullName(null!);

        // Assert ...
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(TestData.AssemblyParameterName);
    }
}

public class GetName
{
    [Fact]
    [Trait("Category", "Unit")]
    public void ReturnsTheAttributeValue_GivenAnAssemblyWithAVersionAttribute()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var result = services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetName(Assembly.GetExecutingAssembly());

        // Assert ...
        result.Should().Be("SimonGeering.Framework.Test");
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void ThrowsException_GivenNullAssembly()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var act = () => services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetName(null!);

        // Assert ...
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(TestData.AssemblyParameterName);
    }
}

public class GetTitle
{
    [Fact]
    [Trait("Category", "Unit")]
    public void ReturnsTheAttributeValue_GivenAnAssemblyWithAVersionAttribute()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var result = services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetTitle(Assembly.GetExecutingAssembly());

        // Assert ...
        result.Should().Be("SimonGeering.Framework.Test");
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void ThrowsException_GivenNullAssembly()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var act = () => services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetTitle(null!);

        // Assert ...
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(TestData.AssemblyParameterName);
    }
}

public class GetCopyright
{
    [Fact]
    [Trait("Category", "Unit")]
    public void ReturnsTheAttributeValue_GivenAnAssemblyWithAVersionAttribute()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var result = services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetCopyright(Assembly.GetExecutingAssembly());

        // Assert ...
        result.Should().Be("Copyright (c) 2020 Simon Geering.");
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void ThrowsException_GivenNullAssembly()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var act = () => services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetCopyright(null!);

        // Assert ...
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(TestData.AssemblyParameterName);
    }
}

public class GetConfiguration
{
    [Fact]
    [Trait("Category", "Unit")]
    public void ThrowsException_GivenNullAssembly()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var act = () => services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetConfiguration(null!);

        // Assert ...
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(TestData.AssemblyParameterName);
    }
}

public class GetTrademark
{
    [Fact]
    [Trait("Category", "Unit")]
    public void ThrowsException_GivenNullAssembly()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var act = () => services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetTrademark(null!);

        // Assert ...
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(TestData.AssemblyParameterName);
    }
}

public class GetFileVersion
{
    [Fact]
    [Trait("Category", "Unit")]
    public void ThrowsException_GivenNullAssembly()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var act = () => services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetFileVersion(null!);

        // Assert ...
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(TestData.AssemblyParameterName);
    }
}

public class GetVersion
{
    [Fact]
    [Trait("Category", "Unit")]
    public void ThrowsException_GivenNullAssembly()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var act = () => services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetVersion(null!);

        // Assert ...
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(TestData.AssemblyParameterName);
    }
}

public class GetProduct
{
    [Fact]
    [Trait("Category", "Unit")]
    public void ReturnsTheAttributeValue_GivenAnAssemblyWithAProductAttribute()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var result = services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetProduct(Assembly.GetExecutingAssembly());

        // Assert ...
        result.Should().Be("Simon Geering - Framework");
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void ThrowsException_GivenNullAssembly()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var act = () => services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetProduct(null!);

        // Assert ...
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(TestData.AssemblyParameterName);
    }
}

public class GetCompany
{
    [Fact]
    [Trait("Category", "Unit")]
    public void ReturnsTheAttributeValue_GivenAnAssemblyWithACompanyAttribute()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var result = services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetCompany(Assembly.GetExecutingAssembly());

        // Assert ...
        result.Should().Be("Test Company Attribute");
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void ThrowsException_GivenNullAssembly()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var act = () => services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetCompany(null!);

        // Assert ...
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(TestData.AssemblyParameterName);
    }
}

public class GetDescription
{
    [Fact]
    [Trait("Category", "Unit")]
    public void ReturnsTheAttributeValue_GivenAnAssemblyWithADescriptionAttribute()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var result = services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetDescription(Assembly.GetExecutingAssembly());

        // Assert ...
        result.Should().Be("Simon Geering - Framework - Tests");
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void ThrowsException_GivenNullAssembly()
    {
        // Arrange ...
        var services = new ServiceCollection();
        services.AddSimonGeeringFramework();

        // Act ...
        var act = () => services.BuildServiceProvider().GetRequiredService<IAssemblyAttributeHelper>().GetDescription(null!);

        // Assert ...
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(TestData.AssemblyParameterName);
    }
}

internal static class TestData
{
    internal const string AssemblyParameterName = "assembly";
}

#pragma warning restore CA1707 // Identifiers should not contain underscores

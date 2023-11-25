#pragma warning disable CA1707 // Identifiers should not contain underscores
using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.Fluent;
using Xunit;

//add a using directive to ArchUnitNET.Fluent.ArchRuleDefinition to easily define ArchRules
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using Xunit.Abstractions;

namespace AdminAssistant.Test.Architecture;

public sealed class Architecture_Test(ITestOutputHelper output)
{
    // https://archunitnet.readthedocs.io/en/latest/guide/
    private static readonly ArchUnitNET.Domain.Architecture _architecture =
        new ArchLoader().LoadAssemblies(typeof(Architecture_Test).Assembly)
        .Build();

    private readonly IObjectProvider<IType> ExampleLayer = Types().That().ResideInAssembly("ExampleAssembly").As("Example Layer");

    [Fact(Skip = "WIP")]
    [Trait("Category", "Unit")]
    public void Return_IsValid_GivenAValidCurrency()
    {
        // Arrange
        var assemblies = _architecture.Assemblies.ToList();

        ExampleLayer.Should().NotBeNull();

        // Act
        assemblies.ForEach( x => output.WriteLine(x.FullName));

        // Assert
        assemblies.Should().HaveCount(1);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores

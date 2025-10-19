#pragma warning disable CA1707 // Identifiers should not contain underscores

using NetArchTest.Rules;
using NetArchTest.Rules.Policies;
using Xunit.Abstractions;

namespace AdminAssistant.Test.Architecture;

public sealed class Architecture_Test(ITestOutputHelper output)
{
    [Fact]
    [Trait("Category", "Architecture")]
    public void DomainModel_Should_OnlyDependOn_Abstractions()
    {
        // Arrange
        var architecturePolicy = Policy.Define("Passing Policy", "This policy demonstrated a valid passing policy with reasonable rules")
            .For(Types.InCurrentDomain)
            .Add(t => t.That()
               .ResideInNamespace("AdminAssistant.DomainModel")
                   .ShouldNot()
                   .HaveDependencyOn("AdminAssistant.UI"),
               "Enforcing clean architecture", "Domain should not depend on UI"
            );

        // Act
        var policyEvaluation = architecturePolicy.Evaluate();

        // Assert
        policyEvaluation.WriteReportTo(output);
        policyEvaluation.HasViolations.ShouldBeFalse();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores

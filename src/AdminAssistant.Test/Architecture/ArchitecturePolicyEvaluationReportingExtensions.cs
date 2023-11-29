#pragma warning disable CA1707 // Identifiers should not contain underscores
using NetArchTest.Rules.Policies;
using Xunit.Abstractions;

namespace AdminAssistant.Test.Architecture
{
    internal static class ArchitecturePolicyEvaluationReportingExtensions
    {
        public static void WriteReportTo(this PolicyResults results, ITestOutputHelper output)
        {
            if (results.HasViolations)
            {
                output.WriteLine($"Policy violations found for: {results.Name}");

                foreach (var rule in results.Results)
                {
                    if (!rule.IsSuccessful)
                    {
                        output.WriteLine("-----------------------------------------------------------");
                        output.WriteLine($"Rule failed: {rule.Name}");

                        foreach (var type in rule.FailingTypes)
                        {
                            output.WriteLine($"\t {type.FullName}");
                        }
                    }
                }

                output.WriteLine("-----------------------------------------------------------");
            }
            else
            {
                output.WriteLine($"No policy violations found for: {results.Name}");
            }
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores

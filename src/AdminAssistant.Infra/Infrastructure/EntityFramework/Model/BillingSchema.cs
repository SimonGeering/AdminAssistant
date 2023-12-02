using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infrastructure.EntityFramework.Model;

internal static class BillingSchema
{
    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "WIP")]
    private const string Name = "Billing";

    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "WIP")]
    internal static void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO: BillingSchema.OnModelCreating
    }
}

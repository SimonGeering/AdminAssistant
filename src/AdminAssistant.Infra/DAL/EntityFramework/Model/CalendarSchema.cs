using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model;

internal class CalendarSchema
{
    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "WIP")]
    private const string Name = "Calendar";

    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "WIP")]
    internal static void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO: CalendarSchema.OnModelCreating
    }
}

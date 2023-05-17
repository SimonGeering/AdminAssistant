using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model;

internal static class TasksSchema
{
    [SuppressMessage("Performance", "CA1823:Avoid unused private fields", Justification = "WIP")]
    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "WIP")]
    private const string Name = "Tasks";

    [SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "WIP")]
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "WIP")]
    internal static void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO: DocumentsSchema.OnModelCreating
    }
}

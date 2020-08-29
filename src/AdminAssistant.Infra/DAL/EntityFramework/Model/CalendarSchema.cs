using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model
{
    internal class CalendarSchema
    {
        [SuppressMessage("Performance", "CA1823:Avoid unused private fields", Justification = "WIP")]
        private const string Name = "Calendar";

        [SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "WIP")]
        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "WIP")]
        internal static void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: CalendarSchema.OnModelCreating
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.EntityFramework.Model
{
    internal class TasksSchema
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1823:Avoid unused private fields", Justification = "WIP")]
        private const string Name = "Tasks";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "WIP")]
        internal static void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: DocumentsSchema.OnModelCreating
        }
    }
}

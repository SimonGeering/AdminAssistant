using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.EntityFramework.Model.Budget
{
    internal class BudgetSchema
    {
        private const string Name = "Budget";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "WIP")]
        internal static void OnModelCreating(ModelBuilder modelBuilder)
        {
            BudgetSchema.Budget_OnModelCreating(modelBuilder);
            BudgetSchema.BudgetEntry_OnModelCreating(modelBuilder);

            // TODO: BudgetSchema.OnModelCreating
        }

        private static void Budget_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetEntity>().ToTable("Budget").Metadata.SetSchema(BudgetSchema.Name);
            modelBuilder.Entity<BudgetEntity>().HasKey(x => x.BudgetID);
            modelBuilder.Entity<BudgetEntity>().Property(x => x.BudgetName).IsRequired().IsUnicode().HasMaxLength(DomainModel.Modules.BudgetModule.Budget.BudgetNameMaxLength);
            // TODO: Budget_OnModelCreating
        }
        private static void BudgetEntry_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetEntryEntity>().ToTable("BudgetEntry").Metadata.SetSchema(BudgetSchema.Name);
            modelBuilder.Entity<BudgetEntryEntity>().HasKey(x => x.BudgetEntryID);
            // TODO: BudgetEntry_OnModelCreating
        }
    }
}

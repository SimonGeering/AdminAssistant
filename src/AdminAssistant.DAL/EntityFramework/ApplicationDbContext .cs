using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.EntityFramework.Model;
using AdminAssistant.DAL.EntityFramework.Model.Budget;
using AdminAssistant.DAL.EntityFramework.Model.Contacts;
using AdminAssistant.DAL.EntityFramework.Model.Core;
using AdminAssistant.DAL.EntityFramework.Model.Documents;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.EntityFramework
{
    public interface IApplicationDbContext : IDisposable
    {
        ICoreSchema Core { get;}
        IAccountsSchema Accounts { get; }

        // TODO: Soft Delete https://medium.com/@unhandlederror/deleting-it-softly-with-ef-core-5f191db5cf72
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void EnsureDatabaseIsCreated();
        void Migrate();
    }

    // dotnet ef migrations add InitialCreate --startup-project ..\AdminAssistant.Accounts.Test\AdminAssistant.Accounts.Test.csproj
    // dotnet ef database update
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ICoreSchema Core => new CoreSchema();
        public IAccountsSchema Accounts => new AccountsSchema();


        public void EnsureDatabaseIsCreated() => this.Database.EnsureCreated();
        public void Migrate() => this.Database.Migrate();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CoreSchema.OnModelCreating(modelBuilder);

            AccountsSchema.OnModelCreating(modelBuilder);
            AssetRegisterSchema.OnModelCreating(modelBuilder);
            BillingSchema.OnModelCreating(modelBuilder);
            BudgetSchema.OnModelCreating(modelBuilder);
            CalendarSchema.OnModelCreating(modelBuilder);
            ContactsSchema.OnModelCreating(modelBuilder);
            // DashboardSchema
            DocumentsSchema.OnModelCreating(modelBuilder);
            // MailSchema
            // ReportsSchema
            TasksSchema.OnModelCreating(modelBuilder);

            // No cascading deletes (Do this last) ...
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().Where(e => !e.IsOwned()).SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}

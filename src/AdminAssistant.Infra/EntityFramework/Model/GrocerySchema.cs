using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.EntityFramework.Model.Grocery
{
    internal class GrocerySchema
    {
        private const string Name = "Grocery";

        internal static void OnModelCreating(ModelBuilder modelBuilder)
        {
            GrocerySchema.GroceryItem_OnModelCreating(modelBuilder);
            // TODO: GrocerySchema.OnModelCreating
        }

        private static void GroceryItem_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroceryItemEntity>().ToTable("GroceryItem").Metadata.SetSchema(GrocerySchema.Name);
            modelBuilder.Entity<GroceryItemEntity>().HasKey(x => x.GroceryItemID);
        }
    }
}

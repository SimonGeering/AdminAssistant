using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Grocery;

internal static class GrocerySchema
{
    private const string Name = "Grocery";

    // TODO: GrocerySchema.OnModelCreating
    internal static void OnModelCreating(ModelBuilder modelBuilder) => GroceryItem_OnModelCreating(modelBuilder);

    private static void GroceryItem_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroceryItemEntity>().ToTable("GroceryItem").Metadata.SetSchema(GrocerySchema.Name);
        modelBuilder.Entity<GroceryItemEntity>().HasKey(x => x.GroceryItemID);
    }
}

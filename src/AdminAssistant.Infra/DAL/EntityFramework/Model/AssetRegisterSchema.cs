using AdminAssistant.Infra.DAL.EntityFramework.Model.AssetRegister;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model;

internal static class AssetRegisterSchema
{
    private const string Name = "Accounts";

    internal static void OnModelCreating(ModelBuilder modelBuilder) => Asset_OnModelCreating(modelBuilder);

    private static void Asset_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetEntity>().ToTable("Asset").Metadata.SetSchema(AssetRegisterSchema.Name);
        modelBuilder.Entity<AssetEntity>().Metadata.SetSchema("AssetRegister");
        modelBuilder.Entity<AssetEntity>().HasKey(x => x.AssetID);
        modelBuilder.Entity<AssetEntity>().Property(x => x.PurchasePrice).IsRequired();
        modelBuilder.Entity<AssetEntity>().Property(x => x.DepreciatedValue).IsRequired();
        modelBuilder.Entity<AssetEntity>().Property(x => x.ReplacementCost).IsRequired();
    }
}

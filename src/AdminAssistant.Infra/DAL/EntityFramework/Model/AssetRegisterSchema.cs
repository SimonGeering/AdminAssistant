using AdminAssistant.DAL.EntityFramework.Model.AssetRegister;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.EntityFramework.Model
{
    internal class AssetRegisterSchema
    {
        private const string Name = "Accounts";

        internal static void OnModelCreating(ModelBuilder modelBuilder)
        {
            AssetRegisterSchema.Asset_OnModelCreating(modelBuilder);
        }

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
}

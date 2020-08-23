/*
Table "Assets.Manufacturer" 
{
  "ManufacturerID" INT [pk]
  "AuditID" INT
  "ManufacturerName" NVARCHAR(50)  
}
Ref: "Assets.Manufacturer"."ManufacturerID" < "Assets.Asset"."ManufacturerID"

*/
namespace AdminAssistant.DAL.EntityFramework.Model.AssetRegister
{
    public class ManufacturerEntity
    {
    }
}

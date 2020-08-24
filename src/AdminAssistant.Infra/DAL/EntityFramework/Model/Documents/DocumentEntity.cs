namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Documents
{
    /*
     Table "Documents.Document" 
{
  "DocumentID" INT [pk]
  "AuditID" INT
  "OwnerID" INT
  "URI" NVARCHAR(255)
}
Ref: "Documents.Document"."DocumentID" < "Accounts.BankAccountStatement"."DocumentID"
Ref: "Documents.Document"."DocumentID" < "Accounts.BankAccountTransactionDocument"."DocumentID"

*/
    public class DocumentEntity
    {
        public int DocumentID { get; set; }
        public int AuditID { get; internal set; }
        public int OwnerID { get; internal set; }

        public Core.AuditEntity Audit { get; internal set; } = null!;
    }
}

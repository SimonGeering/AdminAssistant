using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.MailModule
{
    public record MailMessage : IDatabasePersistable
    {
        public const int SubjectNameMaxLength = Constants.DescriptionMaxLength;

        public int MailMessageID { get; set; }
        public string Subject { get; set; } = string.Empty;

        public int PrimaryKey => MailMessageID;
    }
}

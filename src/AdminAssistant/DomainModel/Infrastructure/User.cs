using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.DomainModel.Infrastructure
{
    public class User
    {
        public const int SignOnMaxLength = 50;

        public int UserID { get; set; } = Constants.UnknownRecordID;
        public string SignOn { get; set; } = string.Empty;
    }
}

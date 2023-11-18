namespace AdminAssistant.DomainModel.Shared;

public sealed record User
{
    public const int SignOnMaxLength = 50;

    public int UserID { get; set; } = Constants.UnknownRecordID;
    public string SignOn { get; set; } = string.Empty;
}

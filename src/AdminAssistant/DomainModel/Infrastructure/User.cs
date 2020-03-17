namespace AdminAssistant.DomainModel.Infrastructure
{
    public class User
    {
        public const int SignOnMaxLength = 50;

        public string SignOn { get; set; } = string.Empty;
    }
}

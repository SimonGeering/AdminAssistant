namespace AdminAssistant.Modules.MailModule.Queries
{
    public sealed record MailMessageQuery : IRequest<Result<IEnumerable<MailMessage>>>;
}

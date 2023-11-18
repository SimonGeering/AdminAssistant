namespace AdminAssistant.DomainModel.Modules.MailModule.CQRS
{
    public sealed record MailMessageQuery : IRequest<Result<IEnumerable<MailMessage>>>;
}

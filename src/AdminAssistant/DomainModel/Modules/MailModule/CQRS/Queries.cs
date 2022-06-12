namespace AdminAssistant.DomainModel.Modules.MailModule.CQRS
{
    public record MailMessageQuery : IRequest<Result<IEnumerable<MailMessage>>>;
}

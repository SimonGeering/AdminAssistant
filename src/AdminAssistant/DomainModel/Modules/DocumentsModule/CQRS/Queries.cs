namespace AdminAssistant.DomainModel.Modules.DocumentsModule.CQRS
{
    public record DocumentQuery : IRequest<Result<IEnumerable<Document>>>;
}
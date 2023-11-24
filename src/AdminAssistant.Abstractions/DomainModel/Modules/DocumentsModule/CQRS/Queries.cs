namespace AdminAssistant.DomainModel.Modules.DocumentsModule.CQRS
{
    public sealed record DocumentQuery : IRequest<Result<IEnumerable<Document>>>;
}

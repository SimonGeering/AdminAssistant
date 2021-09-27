using AdminAssistant.Infra.DAL.Modules.DocumentsModule;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;

namespace AdminAssistant.DomainModel.Modules.DocumentsModule.CQRS;

internal class DocumentQueryHandler : RequestHandlerBase<DocumentQuery, Result<IEnumerable<Document>>>
{
    private readonly IDocumentRepository _repository;

    public DocumentQueryHandler(IDocumentRepository repository, ILoggingProvider loggingProvider)
        : base(loggingProvider) => _repository = repository;

    public override async Task<Result<IEnumerable<Document>>> Handle(DocumentQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetListAsync().ConfigureAwait(false);
        return Result<IEnumerable<Document>>.Success(result);
    }
}

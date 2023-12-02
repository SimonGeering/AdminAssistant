using AdminAssistant.Modules.DocumentsModule.Infrastructure.DAL;

namespace AdminAssistant.Modules.DocumentsModule.Queries;
public sealed record DocumentQuery : IRequest<Result<IEnumerable<Document>>>;

internal sealed class DocumentQueryHandler(IDocumentRepository repository, ILoggingProvider loggingProvider)
    : RequestHandlerBase<DocumentQuery, Result<IEnumerable<Document>>>(loggingProvider)
{
    public override async Task<Result<IEnumerable<Document>>> Handle(DocumentQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetListAsync(cancellationToken).ConfigureAwait(false);
        return Result<IEnumerable<Document>>.Success(result);
    }
}

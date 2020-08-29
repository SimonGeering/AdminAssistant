using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.DocumentsModule.CQRS
{
    public class DocumentQuery : IRequest<Result<IEnumerable<Document>>>
    {
    }
}

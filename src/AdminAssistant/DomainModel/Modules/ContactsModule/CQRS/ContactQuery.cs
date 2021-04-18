using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.ContactsModule.CQRS
{
    public record ContactQuery : IRequest<Result<IEnumerable<Contact>>>;
}

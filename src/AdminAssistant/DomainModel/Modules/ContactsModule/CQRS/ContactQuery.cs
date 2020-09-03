using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.ContactsModule.CQRS
{
    public class ContactQuery : IRequest<Result<IEnumerable<Contact>>>
    {
    }
}

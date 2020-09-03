using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.MailModule.CQRS
{
    public class MailMessageQuery : IRequest<Result<IEnumerable<MailMessage>>>
    {
    }
}

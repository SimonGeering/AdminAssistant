using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.CalendarModule.CQRS
{
    public class ReminderQuery : IRequest<Result<IEnumerable<Reminder>>>
    {
    }
}

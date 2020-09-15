using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AssetRegisterModule.CQRS
{
    public class AssetQuery : IRequest<Result<IEnumerable<Asset>>>
    {
    }
}

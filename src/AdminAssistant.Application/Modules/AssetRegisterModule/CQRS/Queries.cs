using AdminAssistant.DomainModel.Modules.AssetRegisterModule;

namespace AdminAssistant.Application.Modules.AssetRegisterModule.CQRS
{
    public sealed record AssetQuery : IRequest<Result<IEnumerable<Asset>>>;
}

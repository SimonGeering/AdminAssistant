namespace AdminAssistant.DomainModel.Modules.AssetRegisterModule.CQRS
{
    public sealed record AssetQuery : IRequest<Result<IEnumerable<Asset>>>;
}

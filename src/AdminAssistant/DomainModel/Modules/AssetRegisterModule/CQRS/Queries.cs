namespace AdminAssistant.DomainModel.Modules.AssetRegisterModule.CQRS
{
    public record AssetQuery : IRequest<Result<IEnumerable<Asset>>>;
}

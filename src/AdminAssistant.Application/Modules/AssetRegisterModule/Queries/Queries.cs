namespace AdminAssistant.Modules.AssetRegisterModule.Queries;

public sealed record AssetQuery : IRequest<Result<IEnumerable<Asset>>>;

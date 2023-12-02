namespace AdminAssistant.Modules.BudgetModule.Queries;

public sealed record BudgetQuery : IRequest<Result<IEnumerable<Budget>>>;

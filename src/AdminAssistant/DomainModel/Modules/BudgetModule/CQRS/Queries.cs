namespace AdminAssistant.DomainModel.Modules.BudgetModule.CQRS
{
    public sealed record BudgetQuery : IRequest<Result<IEnumerable<Budget>>>;
}

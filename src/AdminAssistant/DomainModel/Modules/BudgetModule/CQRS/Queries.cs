namespace AdminAssistant.DomainModel.Modules.BudgetModule.CQRS
{
    public record BudgetQuery : IRequest<Result<IEnumerable<Budget>>>;
}

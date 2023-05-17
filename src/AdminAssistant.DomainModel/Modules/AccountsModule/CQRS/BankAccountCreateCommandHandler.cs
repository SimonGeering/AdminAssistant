using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal sealed class BankAccountCreateCommandHandler : RequestHandlerBase<BankAccountCreateCommand, Result<BankAccount>>
{
    private readonly IBankAccountRepository bankAccountRepository;
    private readonly IBankAccountValidator bankAccountValidator;

    public BankAccountCreateCommandHandler(ILoggingProvider loggingProvider, IBankAccountRepository bankAccountRepository, IBankAccountValidator bankAccountValidator)
        : base(loggingProvider)
    {
        this.bankAccountRepository = bankAccountRepository;
        this.bankAccountValidator = bankAccountValidator;
    }

    public override async Task<Result<BankAccount>> Handle(BankAccountCreateCommand command, CancellationToken cancellationToken)
    {
        // Don't need this for now as the validator no longer needs extra context.
        // Keep it here for reference of how to do this.
        //
        // var ctx = new FluentValidation.ValidationContext<BankAccount>(command.BankAccount);
        //ctx.RootContextData[Constants.IsCreateCommandContext] = true;

        var validationResult = await bankAccountValidator.ValidateAsync(command.BankAccount, cancellationToken).ConfigureAwait(false);

        if (validationResult.IsValid == false)
        {
            return Result<BankAccount>.Invalid(validationResult.AsErrors());
        }

        var result = await bankAccountRepository.SaveAsync(command.BankAccount).ConfigureAwait(false);
        return Result<BankAccount>.Success(result);
    }
}

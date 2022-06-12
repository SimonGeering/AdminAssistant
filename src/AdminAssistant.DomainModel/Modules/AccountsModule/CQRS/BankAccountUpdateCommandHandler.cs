using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal class BankAccountUpdateCommandHandler : RequestHandlerBase<BankAccountUpdateCommand, Result<BankAccount>>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IBankAccountValidator _bankAccountValidator;

    public BankAccountUpdateCommandHandler(ILoggingProvider loggingProvider, IBankAccountRepository bankAccountRepository, IBankAccountValidator bankAccountValidator)
        : base(loggingProvider)
    {
        _bankAccountRepository = bankAccountRepository;
        _bankAccountValidator = bankAccountValidator;
    }

    public override async Task<Result<BankAccount>> Handle(BankAccountUpdateCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _bankAccountValidator.ValidateAsync(command.BankAccount, cancellationToken).ConfigureAwait(false);

        if (validationResult.IsValid == false)
        {
            return Result<BankAccount>.Invalid(validationResult.AsErrors());
        }

        var result = await _bankAccountRepository.SaveAsync(command.BankAccount).ConfigureAwait(false);
        return Result<BankAccount>.Success(result);
    }
}

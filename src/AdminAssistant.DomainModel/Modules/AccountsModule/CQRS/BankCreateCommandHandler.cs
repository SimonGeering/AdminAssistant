using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal sealed class BankCreateCommandHandler : RequestHandlerBase<BankCreateCommand, Result<Bank>>
{
    private readonly IBankRepository bankRepository;
    private readonly IBankValidator bankValidator;

    public BankCreateCommandHandler(ILoggingProvider loggingProvider, IBankRepository bankRepository, IBankValidator bankValidator)
        : base(loggingProvider)
    {
        this.bankRepository = bankRepository;
        this.bankValidator = bankValidator;
    }

    public override async Task<Result<Bank>> Handle(BankCreateCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await bankValidator.ValidateAsync(command.Bank, cancellationToken).ConfigureAwait(false);

        if (validationResult.IsValid == false)
        {
            return Result<Bank>.Invalid(validationResult.AsErrors());
        }

        var result = await bankRepository.SaveAsync(command.Bank).ConfigureAwait(false);
        return Result<Bank>.Success(result);
    }
}

using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal class BankUpdateCommandHandler : RequestHandlerBase<BankUpdateCommand, Result<Bank>>
{
    private readonly IBankRepository _bankRepository;
    private readonly IBankValidator _bankValidator;

    public BankUpdateCommandHandler(ILoggingProvider loggingProvider, IBankRepository bankRepository, IBankValidator bankValidator)
        : base(loggingProvider)
    {
        _bankRepository = bankRepository;
        _bankValidator = bankValidator;
    }

    public override async Task<Result<Bank>> Handle(BankUpdateCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _bankValidator.ValidateAsync(command.Bank, cancellationToken).ConfigureAwait(false);

        if (validationResult.IsValid == false)
        {
            return Result<Bank>.Invalid(validationResult.AsErrors());
        }

        var result = await _bankRepository.SaveAsync(command.Bank).ConfigureAwait(false);
        return Result<Bank>.Success(result);
    }
}

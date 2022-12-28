using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal sealed class BankAccountTypesQueryHandler : RequestHandlerBase<BankAccountTypesQuery, Result<IEnumerable<BankAccountType>>>
{
    private readonly IBankAccountTypeRepository _bankAccountTypeRepository;

    public BankAccountTypesQueryHandler(IBankAccountTypeRepository bankAccountTypeRepository, ILoggingProvider loggingProvider)
        : base(loggingProvider) => _bankAccountTypeRepository = bankAccountTypeRepository;

    public override async Task<Result<IEnumerable<BankAccountType>>> Handle(BankAccountTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await _bankAccountTypeRepository.GetListAsync().ConfigureAwait(false);

        Trace.Assert(result.Count > 0, "BankAccountType list was not populated.");

        return Result<IEnumerable<BankAccountType>>.Success(result);
    }
}

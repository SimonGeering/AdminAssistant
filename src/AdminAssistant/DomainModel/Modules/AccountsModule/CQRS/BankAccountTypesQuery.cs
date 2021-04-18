using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public record BankAccountTypesQuery : IRequest<Result<IEnumerable<BankAccountType>>>;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountTypesHandler : RequestHandlerBase<BankAccountTypesQuery, Result<IEnumerable<BankAccountType>>>
    {
        private readonly IBankAccountTypeRepository _bankAccountTypeRepository;

        public BankAccountTypesHandler(IBankAccountTypeRepository bankAccountTypeRepository, ILoggingProvider loggingProvider)
            : base(loggingProvider) => _bankAccountTypeRepository = bankAccountTypeRepository;

        public override async Task<Result<IEnumerable<BankAccountType>>> Handle(BankAccountTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _bankAccountTypeRepository.GetListAsync().ConfigureAwait(false);

            Trace.Assert(result.Count > 0, "BankAccountType list was not populated.");

            return Result<IEnumerable<BankAccountType>>.Success(result);
        }
    }
}

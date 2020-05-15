using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class BankAccountGetByIDHandler : IRequestHandler<GetBankAccountByIDQuery, Result<BankAccount>>
    {
        private readonly IBankAccountRepository bankAccountRepository;

        public BankAccountGetByIDHandler(IBankAccountRepository bankAccountRepository)
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public async Task<Result<BankAccount>> Handle(GetBankAccountByIDQuery request, CancellationToken cancellationToken)
        {
            var bankAccount = await bankAccountRepository.GetBankAccountAsync(request.BankAccountID).ConfigureAwait(false);

            if (bankAccount == null || bankAccount.BankAccountID == Constants.UnknownRecordID)
                return Result<BankAccount>.NotFound();

            return Result<BankAccount>.Success(bankAccount);
        }
    }
}

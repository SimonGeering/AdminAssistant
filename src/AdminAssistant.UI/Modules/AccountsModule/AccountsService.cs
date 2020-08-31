using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using AutoMapper;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class AccountsService : ServiceBase, IAccountsService
    {
        public AccountsService(IAdminAssistantWebAPIClient adminAssistantWebAPIClient, IMapper mapper, ILoggingProvider log)
            : base(adminAssistantWebAPIClient, mapper, log)
        {
        }
        public async Task<List<BankAccountType>> LoadBankAccountTypesLookupDataAsync()
        {
            Log.Start();

            var response = await AdminAssistantWebAPIClient.GetBankAccountTypeAsync().ConfigureAwait(false);

            var result = new List<BankAccountType>(Mapper.Map<IEnumerable<BankAccountType>>(response));
            result.Insert(0, new BankAccountType() { BankAccountTypeID = 0, Description = string.Empty });

            return Log.Finish(result);
        }

        public async Task<BankAccount> CreateBankAccountAsync(BankAccount model)
        {
            Log.Start();

            var request = Mapper.Map<BankAccountCreateRequestDto>(model);

            var response = await AdminAssistantWebAPIClient.PostBankAccountAsync(request).ConfigureAwait(false);

            var result = Mapper.Map<BankAccount>(response);
            return Log.Finish(result);
        }

        public async Task<BankAccount> UpdateBankAccountAsync(BankAccount model)
        {
            Log.Start();

            var request = Mapper.Map<BankAccountUpdateRequestDto>(model);

            var response = await AdminAssistantWebAPIClient.PutBankAccountAsync(request).ConfigureAwait(false);

            var result = Mapper.Map<BankAccount>(response);
            return Log.Finish(result);
        }
    }
}

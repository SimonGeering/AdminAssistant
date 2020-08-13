using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
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
        public async Task<IList<BankAccountType>> LoadBankAccountTypesLookupDataAsync()
        {
            this.Log.Start();

            var response = await this.AdminAssistantWebAPIClient.GetBankAccountTypeAsync().ConfigureAwait(false);

            var result = new List<BankAccountType>(this.Mapper.Map<IEnumerable<BankAccountType>>(response));
            result.Insert(0, new BankAccountType() { BankAccountTypeID = 0, Description = string.Empty });

            return this.Log.Finish(result);
        }

        public async Task<BankAccount> CreateBankAccountAsync(BankAccount model)
        {
            this.Log.Start();

            var request = this.Mapper.Map<BankAccountCreateRequestDto>(model);

            var response = await this.AdminAssistantWebAPIClient.PostBankAccountAsync(request).ConfigureAwait(false);

            var result = this.Mapper.Map<BankAccount>(response);
            return this.Log.Finish(result);
        }

        public async Task<BankAccount> UpdateBankAccountAsync(BankAccount model)
        {
            this.Log.Start();

            var request = this.Mapper.Map<BankAccountUpdateRequestDto>(model);

            var response = await this.AdminAssistantWebAPIClient.PutBankAccountAsync(request).ConfigureAwait(false);

            var result = this.Mapper.Map<BankAccount>(response);
            return this.Log.Finish(result);
        }
    }
}

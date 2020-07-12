using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.Framework.Providers;
using AdminAssistant.WebAPI.v1;
using AutoMapper;
using System;
using System.Net.Http.Json;

namespace AdminAssistant.UI.Modules.Accounts
{
    public class AccountsService : ServiceBase, IAccountsService
    {
        public AccountsService(IHttpClientJsonProvider httpClient, IMapper mapper, ILoggingProvider log)
            : base(httpClient, mapper, log)
        {
        }
        public async Task<IList<BankAccountType>> LoadBankAccountTypesLookupDataAsync()
        {
            this.Log.Start();

            var response = await this.HttpClient.GetFromJsonAsync<BankAccountTypeResponseDto[]>("api/v1/BankAccountType").ConfigureAwait(false);

            var result = new List<BankAccountType>(this.Mapper.Map<IEnumerable<BankAccountType>>(response));
            result.Insert(0, new BankAccountType() { BankAccountTypeID = 0, Description = string.Empty });

            return this.Log.Finish(result);
        }

        public async Task<IList<Currency>> LoadCurrencyLookupDataAsync()
        {
            this.Log.Start();

            var response = await this.HttpClient.GetFromJsonAsync<CurrencyResponseDto[]>("api/v1/Currency").ConfigureAwait(false);

            var result = new List<Currency>(this.Mapper.Map<IEnumerable<Currency>>(response));
            result.Insert(0, new Currency() { CurrencyID = 0, Symbol = string.Empty, DecimalFormat = string.Empty });

            return this.Log.Finish(result);
        }

        public async Task<BankAccount> CreateBankAccountAsync(BankAccount model)
        {
            this.Log.Start();

            var request = this.Mapper.Map<BankAccountCreateRequestDto>(model);

            var response = await this.HttpClient.PostAsJsonAsync("api/v1/BankAccount", request).ConfigureAwait(false);

            //var result = this.Mapper.Map<BankAccount>(response.);

            return null!; //this.Log.Finish(result);
        }

        public async Task<BankAccount> UpdateBankAccountAsync(BankAccount model)
        {
            this.Log.Start();

            var request = this.Mapper.Map<BankAccountUpdateRequestDto>(model);

            var response = await this.HttpClient.PutAsJsonAsync("api/v1/BankAccount", request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode == false)
            {
                throw new System.NotImplementedException();
            }

            var responseDto = await response.Content.ReadFromJsonAsync<BankAccountResponseDto>().ConfigureAwait(false);
            var result = this.Mapper.Map<BankAccount>(responseDto);
            return this.Log.Finish(result);
        }
    }
}

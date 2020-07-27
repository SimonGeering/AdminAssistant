using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1
{
    public class BankResponseDto : IMapFrom<Bank>, IMapTo<Bank>
    {
        public int BankID { get; set; } = Constants.UnknownRecordID;
        public string BankName { get; set; } = string.Empty;
    }
}

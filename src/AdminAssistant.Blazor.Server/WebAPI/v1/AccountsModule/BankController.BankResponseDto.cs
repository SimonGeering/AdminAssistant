using AdminAssistant.Modules.AccountsModule;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

public sealed record BankResponseDto : IMapFrom<Bank>
{
    public int BankID { get; init; } = Constants.UnknownRecordID;
    public string BankName { get; init; } = string.Empty;

    public void MapFrom(AutoMapper.Profile profile) => profile
        .CreateMap<Bank, BankResponseDto>()
        .ForMember(x => x.BankName, opt => opt.MapFrom(s => s.BankName.Value));
}

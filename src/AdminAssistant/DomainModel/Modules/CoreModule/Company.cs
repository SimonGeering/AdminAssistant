namespace AdminAssistant.DomainModel.Modules.CoreModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "WIP Code")]
    public record Company
    {
        public const int CompanyNumberMaxLength = 50;
        public const int VATNumberMaxLength = 50;
    }
}

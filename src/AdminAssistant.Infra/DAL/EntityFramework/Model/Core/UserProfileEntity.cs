namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Core
{
    public class UserProfileEntity
    {
        public int UserProfileID { get; set; }
        public int AuditID { get; internal set; }
        public string SignOn { get; set; } = string.Empty;
        public string MSGraphID { get; set; } = string.Empty;

        public IList<UserProfilePermissionEntity> Permissions { get; } = new List<UserProfilePermissionEntity>();
        public IList<UserProfileSettingEntity> Settings { get; } = new List<UserProfileSettingEntity>();

        public IList<CompanyEntity> Companies { get; } = new List<CompanyEntity>();
        public PersonalDetailsEntity PersonalDetails { get; set; } = null!;
        public AuditEntity Audit { get; set; } = null!;
    }
}

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Core
{
    public class UserProfileSettingEntity
    {
        public int UserProfileSettingID { get; set; }
        public int UserProfileID { get; set; }
        public int SettingID { get; set; }

        public UserProfileEntity UserProfile { get; set; } = null!;
        public SettingEntity Setting { get; internal set; } = null!;
    }
}

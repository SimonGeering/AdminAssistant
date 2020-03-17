namespace AdminAssistant.DAL.EntityFramework.Model
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

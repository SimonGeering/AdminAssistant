namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Core
{
    public class SettingEntity
    {
        public int SettingID { get; set; }
        public string SettingKey { get; set; } = string.Empty;
        public IList<UserProfileSettingEntity> UserProfileSettings { get; internal set; } = new List<UserProfileSettingEntity>();
    }
}

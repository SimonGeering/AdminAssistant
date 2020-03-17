using System.Collections.Generic;

namespace AdminAssistant.Accounts.DAL.EntityFramework.Model
{
    public class SettingEntity
    {
        public int SettingID { get; set; }
        public string SettingKey { get; set; } = string.Empty;
        public IList<UserProfileSettingEntity> UserProfileSettings { get; internal set; } = new List<UserProfileSettingEntity>();
    }
}

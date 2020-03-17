using System.Collections.Generic;

namespace AdminAssistant.Accounts.DAL.EntityFramework.Model
{
    public class PermissionEntity
    {
        public int PermissionID { get; set; }

        public string PermissionKey { get; set; } = string.Empty;
        public IList<UserProfilePermissionEntity> UserProfilePermissions { get; internal set; } = new List<UserProfilePermissionEntity>();
    }
}

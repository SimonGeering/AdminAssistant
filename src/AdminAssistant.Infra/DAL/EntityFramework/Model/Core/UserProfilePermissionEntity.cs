namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Core;

public sealed class UserProfilePermissionEntity
{
    public int UserProfilePermissionID { get; set; }
    public int UserProfileID { get; set; }
    public int PermissionID { get; set; }

    public UserProfileEntity UserProfile { get; set; } = null!;
    public PermissionEntity Permission { get; internal set; } = null!;
}

using System.Collections.Generic;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Core
{
    public class PersonalDetailsEntity
    {
        public int PersonalDetailsID { get; set; }
        public int AuditID { get; internal set; }
        public int UserProfileID { get; internal set; }
        public AuditEntity Audit { get; set; } = null!;
        public UserProfileEntity UserProfile { get; set; } = null!;
        public IList<OwnerEntity> Owns { get; internal set; } = new List<OwnerEntity>();
    }
}

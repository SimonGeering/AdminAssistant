using System;
using System.Collections.Generic;

namespace AdminAssistant.DAL.EntityFramework.Model
{
    public class PersonalDetailsEntity
    {
        public int PersonalDetailsID { get; set; }
        public int AuditID { get; internal set; }
        public int UserProfileID { get; internal set; }
        public AuditEntity Audit { get; internal set; } = null!;
        public UserProfileEntity UserProfile { get; internal set; } = null!;
        public IList<OwnerEntity> Owns { get; internal set; } = new List<OwnerEntity>();
    }
}

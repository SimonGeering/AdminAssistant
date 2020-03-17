using System;
using System.Collections.Generic;

namespace AdminAssistant.DAL.EntityFramework.Model
{
    public class CompanyEntity
    {
        public int CompanyID { get; set; }
        public int AuditID { get; internal set; }
        public int UserProfileID { get; internal set; }
        public string Name { get; set; } = string.Empty;
        public string CompanyNumber { get; set; } = string.Empty;
        public string VATNumber { get; set; } = string.Empty;
        public DateTime DateOfIncorporation { get; set; }

        public AuditEntity Audit { get; internal set; } = null!;
        public UserProfileEntity UserProfile { get; internal set; } = null!;
        public IList<OwnerEntity> Owns { get; internal set; } = new List<OwnerEntity>();
    }
}

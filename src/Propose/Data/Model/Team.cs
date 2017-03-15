using System;
using Propose.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Propose.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Team: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        
		[Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }

        public ICollection<TeamLeader> TeamLeaders { get; set; } = new HashSet<TeamLeader>();
        
		public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
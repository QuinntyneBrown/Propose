using System;
using System.Collections.Generic;
using Propose.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Propose.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class IdeaLink: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [ForeignKey("Idea")]
        public int? IdeaId { get; set; }

        [Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }
        
		public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual Idea Idea { get; set; }
    }
}

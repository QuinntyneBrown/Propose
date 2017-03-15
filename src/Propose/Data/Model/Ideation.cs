using System;
using System.Collections.Generic;
using Propose.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Propose.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Ideation: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        
		[Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }
        
        public DateTime? Start { get; set; }

        public DateTime End { get; set; }

        public int? MaxiumumVotesPerUser { get; set; }

        public int? MaxiumIdeasPerUser { get; set; }
        
        public ICollection<Idea> Ideas { get; set; }

		public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}

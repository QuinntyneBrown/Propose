using System;
using Propose.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Propose.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Idea: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        [ForeignKey("Ideation")]
        public int? IdeationId { get; set; }

        public string Name { get; set; }

        public string HtmlBody { get; set; }

        public ICollection<IdeaDigitalAsset> IdeaDigitalAssets { get; set; } = new HashSet<IdeaDigitalAsset>();

        public ICollection<IdeaLink> IdeaLinks { get; set; } = new HashSet<IdeaLink>();

        public string HtmlDescription { get; set; }

        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }

        public ICollection<Vote> Votes { get; set; } = new HashSet<Vote>();

		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual User User { get; set; }

        public virtual Ideation Ideation { get; set; }
    }
}

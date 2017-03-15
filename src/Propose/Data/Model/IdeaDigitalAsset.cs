using System;
using Propose.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Propose.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class IdeaDigitalAsset: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [ForeignKey("Idea")]
        public int? IdeaId { get; set; }

        public bool IsFeatured { get; set; }

        public string DigitalAssetUrl { get; set; }
        
		public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual Idea Idea { get; set; }

    }
}

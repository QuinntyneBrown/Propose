using Propose.Data.Helpers;
using static Propose.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Propose.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class User: ILoggable
    {
        public int Id { get; set; }

        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(MaxStringLength)]
        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Fullname { get { return $"{Firstname} {Lastname}"; } }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();

        public virtual Tenant Tenant { get; set; }

        public ICollection<Vote> Votes { get; set; } = new HashSet<Vote>();

        public ICollection<Idea> Ideas { get; set; } = new HashSet<Idea>();
    }
}

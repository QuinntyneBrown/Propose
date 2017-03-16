using Propose.Data.Helpers;
using Propose.Data.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;

namespace Propose.Data
{
    public interface IProposeContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Tenant> Tenants { get; set; }
        DbSet<Idea> Ideas { get; set; }
        DbSet<Ideation> Ideations { get; set; }
        DbSet<Vote> Votes { get; set; }
        DbSet<Team> Teams { get; set; }
        DbSet<IdeaDigitalAsset> IdeaDigitalAssets { get; set; }
        DbSet<DigitalAsset> DigitalAssets { get; set; }
        DbSet<IdeaLink> IdeaLinks { get; set; }
        Task<int> SaveChangesAsync();
    }

    public class ProposeContext: DbContext, IProposeContext
    {
        public ProposeContext()
            :base("ProposeContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Ideation> Ideations { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<IdeaDigitalAsset> IdeaDigitalAssets { get; set; }
        public DbSet<DigitalAsset> DigitalAssets { get; set; }
        public DbSet<IdeaLink> IdeaLinks { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public override int SaveChanges()
        {
            UpdateLoggableEntries();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            UpdateLoggableEntries();
            return base.SaveChangesAsync();
        }

        public void UpdateLoggableEntries()
        {
            foreach (var entity in ChangeTracker.Entries()
                .Where(e => e.Entity is ILoggable && ((e.State == EntityState.Added || (e.State == EntityState.Modified))))
                .Select(x => x.Entity as ILoggable))
            {
                entity.CreatedOn = entity.CreatedOn == default(DateTime) ? DateTime.UtcNow : entity.CreatedOn;
                entity.LastModifiedOn = DateTime.UtcNow;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().
                HasMany(u => u.Roles).
                WithMany(r => r.Users).
                Map(
                    m =>
                    {
                        m.MapLeftKey("User_Id");
                        m.MapRightKey("Role_Id");
                        m.ToTable("UserRoles");
                    });

            var convention = new AttributeToTableAnnotationConvention<SoftDeleteAttribute, string>(
                "SoftDeleteColumnName",
                (type, attributes) => attributes.Single().ColumnName);

            modelBuilder.Conventions.Add(convention);
        }
    }
}
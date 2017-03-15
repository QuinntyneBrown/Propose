using System.Data.Entity.Migrations;
using Propose.Data;
using Propose.Data.Model;

namespace Propose.Migrations
{
    public class TenantConfiguration
    {
        public static void Seed(ProposeContext context) {

            context.Tenants.AddOrUpdate(x => x.Name, new Tenant()
            {
                Name = "Default"
            });

            context.SaveChanges();
        }
    }
}

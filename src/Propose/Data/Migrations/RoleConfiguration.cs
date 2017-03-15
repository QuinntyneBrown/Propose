using System.Data.Entity.Migrations;
using Propose.Data;
using Propose.Data.Model;
using Propose.Features.Users;

namespace Propose.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(ProposeContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.PRODUCT
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}

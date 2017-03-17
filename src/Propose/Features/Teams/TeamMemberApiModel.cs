using Propose.Data.Model;

namespace Propose.Features.Teams
{
    public class TeamMemberApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromTeamMember<TModel>(TeamMember teamMember) where
            TModel : TeamMemberApiModel, new()
        {
            var model = new TModel();
            model.Id = teamMember.Id;
            model.TenantId = teamMember.TenantId;
            model.Name = teamMember.Name;
            return model;
        }

        public static TeamMemberApiModel FromTeamMember(TeamMember teamMember)
            => FromTeamMember<TeamMemberApiModel>(teamMember);

    }
}

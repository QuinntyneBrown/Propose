using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Teams
{
    public class GetTeamMembersQuery
    {
        public class GetTeamMembersRequest : IRequest<GetTeamMembersResponse> { 
            public int? TenantId { get; set; }        
        }

        public class GetTeamMembersResponse
        {
            public ICollection<TeamMemberApiModel> TeamMembers { get; set; } = new HashSet<TeamMemberApiModel>();
        }

        public class GetTeamMembersHandler : IAsyncRequestHandler<GetTeamMembersRequest, GetTeamMembersResponse>
        {
            public GetTeamMembersHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetTeamMembersResponse> Handle(GetTeamMembersRequest request)
            {
                var teamMembers = await _context.TeamMembers
                    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetTeamMembersResponse()
                {
                    TeamMembers = teamMembers.Select(x => TeamMemberApiModel.FromTeamMember(x)).ToList()
                };
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }

    }

}

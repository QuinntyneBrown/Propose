using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Teams
{
    public class GetTeamMembersByUserIdQuery
    {
        public class GetTeamMembersByUserIdRequest : IRequest<GetTeamMembersByUserIdResponse>
        {
            public int? UserId { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetTeamMembersByUserIdResponse
        {
            public ICollection<TeamMemberApiModel> TeamMembers { get; set; } = new HashSet<TeamMemberApiModel>();
        }

        public class GetTeamMembersByUserIdHandler : IAsyncRequestHandler<GetTeamMembersByUserIdRequest, GetTeamMembersByUserIdResponse>
        {
            public GetTeamMembersByUserIdHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetTeamMembersByUserIdResponse> Handle(GetTeamMembersByUserIdRequest request)
            {
                throw new System.NotImplementedException();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }
    }
}

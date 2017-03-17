using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Teams
{
    public class GetTeamMemberByIdQuery
    {
        public class GetTeamMemberByIdRequest : IRequest<GetTeamMemberByIdResponse> { 
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetTeamMemberByIdResponse
        {
            public TeamMemberApiModel TeamMember { get; set; } 
        }

        public class GetTeamMemberByIdHandler : IAsyncRequestHandler<GetTeamMemberByIdRequest, GetTeamMemberByIdResponse>
        {
            public GetTeamMemberByIdHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetTeamMemberByIdResponse> Handle(GetTeamMemberByIdRequest request)
            {                
                return new GetTeamMemberByIdResponse()
                {
                    TeamMember = TeamMemberApiModel.FromTeamMember(await _context.TeamMembers.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }

    }

}

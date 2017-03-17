using MediatR;
using Propose.Data;
using Propose.Data.Model;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Teams
{
    public class RemoveTeamMemberCommand
    {
        public class RemoveTeamMemberRequest : IRequest<RemoveTeamMemberResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveTeamMemberResponse { }

        public class RemoveTeamMemberHandler : IAsyncRequestHandler<RemoveTeamMemberRequest, RemoveTeamMemberResponse>
        {
            public RemoveTeamMemberHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveTeamMemberResponse> Handle(RemoveTeamMemberRequest request)
            {
                var teamMember = await _context.TeamMembers.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                teamMember.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveTeamMemberResponse();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }
    }
}

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
    public class RemoveTeamCommand
    {
        public class RemoveTeamRequest : IRequest<RemoveTeamResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveTeamResponse { }

        public class RemoveTeamHandler : IAsyncRequestHandler<RemoveTeamRequest, RemoveTeamResponse>
        {
            public RemoveTeamHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveTeamResponse> Handle(RemoveTeamRequest request)
            {
                var team = await _context.Teams.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                team.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveTeamResponse();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }
    }
}

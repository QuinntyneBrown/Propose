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
    public class AddOrUpdateTeamMemberCommand
    {
        public class AddOrUpdateTeamMemberRequest : IRequest<AddOrUpdateTeamMemberResponse>
        {
            public TeamMemberApiModel TeamMember { get; set; }
            public int? TenantId { get; set; }
        }

        public class AddOrUpdateTeamMemberResponse { }

        public class AddOrUpdateTeamMemberHandler : IAsyncRequestHandler<AddOrUpdateTeamMemberRequest, AddOrUpdateTeamMemberResponse>
        {
            public AddOrUpdateTeamMemberHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateTeamMemberResponse> Handle(AddOrUpdateTeamMemberRequest request)
            {
                var entity = await _context.TeamMembers
                    .SingleOrDefaultAsync(x => x.Id == request.TeamMember.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.TeamMembers.Add(entity = new TeamMember());
                entity.Name = request.TeamMember.Name;
                entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateTeamMemberResponse();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }

    }

}

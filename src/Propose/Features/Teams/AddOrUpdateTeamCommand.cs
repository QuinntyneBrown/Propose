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
    public class AddOrUpdateTeamCommand
    {
        public class AddOrUpdateTeamRequest : IRequest<AddOrUpdateTeamResponse>
        {
            public TeamApiModel Team { get; set; }
            public int? TenantId { get; set; }
        }

        public class AddOrUpdateTeamResponse { }

        public class AddOrUpdateTeamHandler : IAsyncRequestHandler<AddOrUpdateTeamRequest, AddOrUpdateTeamResponse>
        {
            public AddOrUpdateTeamHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateTeamResponse> Handle(AddOrUpdateTeamRequest request)
            {
                var entity = await _context.Teams
                    .SingleOrDefaultAsync(x => x.Id == request.Team.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Teams.Add(entity = new Team());
                entity.Name = request.Team.Name;
                entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateTeamResponse();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }

    }

}

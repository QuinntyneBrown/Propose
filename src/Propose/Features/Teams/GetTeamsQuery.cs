using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Teams
{
    public class GetTeamsQuery
    {
        public class GetTeamsRequest : IRequest<GetTeamsResponse> { 
            public int? TenantId { get; set; }        
        }

        public class GetTeamsResponse
        {
            public ICollection<TeamApiModel> Teams { get; set; } = new HashSet<TeamApiModel>();
        }

        public class GetTeamsHandler : IAsyncRequestHandler<GetTeamsRequest, GetTeamsResponse>
        {
            public GetTeamsHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetTeamsResponse> Handle(GetTeamsRequest request)
            {
                var teams = await _context.Teams
                    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetTeamsResponse()
                {
                    Teams = teams.Select(x => TeamApiModel.FromTeam(x)).ToList()
                };
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }

    }

}

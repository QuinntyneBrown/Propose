using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Teams
{
    public class GetTeamByIdQuery
    {
        public class GetTeamByIdRequest : IRequest<GetTeamByIdResponse> { 
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetTeamByIdResponse
        {
            public TeamApiModel Team { get; set; } 
        }

        public class GetTeamByIdHandler : IAsyncRequestHandler<GetTeamByIdRequest, GetTeamByIdResponse>
        {
            public GetTeamByIdHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetTeamByIdResponse> Handle(GetTeamByIdRequest request)
            {                
                return new GetTeamByIdResponse()
                {
                    Team = TeamApiModel.FromTeam(await _context.Teams.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }

    }

}

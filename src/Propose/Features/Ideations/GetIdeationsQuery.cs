using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Ideations
{
    public class GetIdeationsQuery
    {
        public class GetIdeationsRequest : IRequest<GetIdeationsResponse> { 
            public int? TenantId { get; set; }        
        }

        public class GetIdeationsResponse
        {
            public ICollection<IdeationApiModel> Ideations { get; set; } = new HashSet<IdeationApiModel>();
        }

        public class GetIdeationsHandler : IAsyncRequestHandler<GetIdeationsRequest, GetIdeationsResponse>
        {
            public GetIdeationsHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetIdeationsResponse> Handle(GetIdeationsRequest request)
            {
                var ideations = await _context.Ideations
                    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetIdeationsResponse()
                {
                    Ideations = ideations.Select(x => IdeationApiModel.FromIdeation(x)).ToList()
                };
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }

    }

}

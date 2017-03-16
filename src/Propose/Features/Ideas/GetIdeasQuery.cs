using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Ideas
{
    public class GetIdeasQuery
    {
        public class GetIdeasRequest : IRequest<GetIdeasResponse> { 
            public int? TenantId { get; set; }        
        }

        public class GetIdeasResponse
        {
            public ICollection<IdeaApiModel> Ideas { get; set; } = new HashSet<IdeaApiModel>();
        }

        public class GetIdeasHandler : IAsyncRequestHandler<GetIdeasRequest, GetIdeasResponse>
        {
            public GetIdeasHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetIdeasResponse> Handle(GetIdeasRequest request)
            {
                var ideas = await _context.Ideas
                    .Include(x => x.User)
                    .Include(x => x.Votes)
                    .Include(x => x.IdeaDigitalAssets)
                    .Include(x => x.IdeaLinks)
                    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetIdeasResponse()
                {
                    Ideas = ideas.Select(x => IdeaApiModel.FromIdea(x)).ToList()
                };
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }
    }
}

using MediatR;
using Propose.Data;
using Propose.Data.Model;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Ideas
{
    public class RemoveIdeaCommand
    {
        public class RemoveIdeaRequest : IRequest<RemoveIdeaResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveIdeaResponse { }

        public class RemoveIdeaHandler : IAsyncRequestHandler<RemoveIdeaRequest, RemoveIdeaResponse>
        {
            public RemoveIdeaHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveIdeaResponse> Handle(RemoveIdeaRequest request)
            {
                var idea = await _context.Ideas.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                idea.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveIdeaResponse();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }
    }
}

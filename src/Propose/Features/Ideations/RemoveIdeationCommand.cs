using MediatR;
using Propose.Data;
using Propose.Data.Model;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Ideations
{
    public class RemoveIdeationCommand
    {
        public class RemoveIdeationRequest : IRequest<RemoveIdeationResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveIdeationResponse { }

        public class RemoveIdeationHandler : IAsyncRequestHandler<RemoveIdeationRequest, RemoveIdeationResponse>
        {
            public RemoveIdeationHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveIdeationResponse> Handle(RemoveIdeationRequest request)
            {
                var ideation = await _context.Ideations.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                ideation.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveIdeationResponse();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }
    }
}

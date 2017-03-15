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
    public class AddOrUpdateIdeationCommand
    {
        public class AddOrUpdateIdeationRequest : IRequest<AddOrUpdateIdeationResponse>
        {
            public IdeationApiModel Ideation { get; set; }
            public int? TenantId { get; set; }
        }

        public class AddOrUpdateIdeationResponse { }

        public class AddOrUpdateIdeationHandler : IAsyncRequestHandler<AddOrUpdateIdeationRequest, AddOrUpdateIdeationResponse>
        {
            public AddOrUpdateIdeationHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateIdeationResponse> Handle(AddOrUpdateIdeationRequest request)
            {
                var entity = await _context.Ideations
                    .SingleOrDefaultAsync(x => x.Id == request.Ideation.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Ideations.Add(entity = new Ideation());
                entity.Name = request.Ideation.Name;
                entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateIdeationResponse();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }

    }

}

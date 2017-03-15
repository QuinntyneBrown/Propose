using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Ideations
{
    public class GetIdeationByIdQuery
    {
        public class GetIdeationByIdRequest : IRequest<GetIdeationByIdResponse> { 
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetIdeationByIdResponse
        {
            public IdeationApiModel Ideation { get; set; } 
        }

        public class GetIdeationByIdHandler : IAsyncRequestHandler<GetIdeationByIdRequest, GetIdeationByIdResponse>
        {
            public GetIdeationByIdHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetIdeationByIdResponse> Handle(GetIdeationByIdRequest request)
            {                
                return new GetIdeationByIdResponse()
                {
                    Ideation = IdeationApiModel.FromIdeation(await _context.Ideations.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }

    }

}

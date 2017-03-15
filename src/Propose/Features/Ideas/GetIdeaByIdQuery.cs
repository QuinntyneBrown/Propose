using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Ideas
{
    public class GetIdeaByIdQuery
    {
        public class GetIdeaByIdRequest : IRequest<GetIdeaByIdResponse> { 
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetIdeaByIdResponse
        {
            public IdeaApiModel Idea { get; set; } 
        }

        public class GetIdeaByIdHandler : IAsyncRequestHandler<GetIdeaByIdRequest, GetIdeaByIdResponse>
        {
            public GetIdeaByIdHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetIdeaByIdResponse> Handle(GetIdeaByIdRequest request)
            {                
                return new GetIdeaByIdResponse()
                {
                    Idea = IdeaApiModel.FromIdea(await _context.Ideas.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }

    }

}

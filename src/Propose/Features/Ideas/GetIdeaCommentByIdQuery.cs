using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Ideas
{
    public class GetIdeaCommentByIdQuery
    {
        public class GetIdeaCommentByIdRequest : IRequest<GetIdeaCommentByIdResponse> { 
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetIdeaCommentByIdResponse
        {
            public IdeaCommentApiModel IdeaComment { get; set; } 
        }

        public class GetIdeaCommentByIdHandler : IAsyncRequestHandler<GetIdeaCommentByIdRequest, GetIdeaCommentByIdResponse>
        {
            public GetIdeaCommentByIdHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetIdeaCommentByIdResponse> Handle(GetIdeaCommentByIdRequest request)
            {                
                return new GetIdeaCommentByIdResponse()
                {
                    IdeaComment = IdeaCommentApiModel.FromIdeaComment(await _context.IdeaComments.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }

    }

}

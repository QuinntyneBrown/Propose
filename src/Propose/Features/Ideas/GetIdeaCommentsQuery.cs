using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Propose.Features.Ideas
{
    public class GetIdeaCommentsQuery
    {
        public class GetIdeaCommentsRequest : IRequest<GetIdeaCommentsResponse> { 
            public int? TenantId { get; set; }        
        }

        public class GetIdeaCommentsResponse
        {
            public ICollection<IdeaCommentApiModel> IdeaComments { get; set; } = new HashSet<IdeaCommentApiModel>();
        }

        public class GetIdeaCommentsHandler : IAsyncRequestHandler<GetIdeaCommentsRequest, GetIdeaCommentsResponse>
        {
            public GetIdeaCommentsHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetIdeaCommentsResponse> Handle(GetIdeaCommentsRequest request)
            {
                var ideaComments = await _context.IdeaComments
                    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetIdeaCommentsResponse()
                {
                    IdeaComments = ideaComments.Select(x => IdeaCommentApiModel.FromIdeaComment(x)).ToList()
                };
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }

    }

}

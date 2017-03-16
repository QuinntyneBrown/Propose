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
    public class RemoveIdeaCommentCommand
    {
        public class RemoveIdeaCommentRequest : IRequest<RemoveIdeaCommentResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveIdeaCommentResponse { }

        public class RemoveIdeaCommentHandler : IAsyncRequestHandler<RemoveIdeaCommentRequest, RemoveIdeaCommentResponse>
        {
            public RemoveIdeaCommentHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveIdeaCommentResponse> Handle(RemoveIdeaCommentRequest request)
            {
                var ideaComment = await _context.IdeaComments.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                ideaComment.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveIdeaCommentResponse();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }
    }
}

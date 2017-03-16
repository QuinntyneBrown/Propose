using MediatR;
using Propose.Data;
using Propose.Data.Model;
using Propose.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Propose.Features.Ideas
{
    public class AddOrUpdateIdeaCommentCommand
    {
        public class AddOrUpdateIdeaCommentRequest : IRequest<AddOrUpdateIdeaCommentResponse>
        {
            public IdeaCommentApiModel IdeaComment { get; set; }
            public int? TenantId { get; set; }
        }

        public class AddOrUpdateIdeaCommentResponse { }

        public class AddOrUpdateIdeaCommentHandler : IAsyncRequestHandler<AddOrUpdateIdeaCommentRequest, AddOrUpdateIdeaCommentResponse>
        {
            public AddOrUpdateIdeaCommentHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateIdeaCommentResponse> Handle(AddOrUpdateIdeaCommentRequest request)
            {
                var entity = await _context.IdeaComments
                    .SingleOrDefaultAsync(x => x.Id == request.IdeaComment.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.IdeaComments.Add(entity = new IdeaComment());
                entity.HtmlBody = request.IdeaComment.HtmlBody;
                entity.IdeaId = request.IdeaComment.IdeaId;
                entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateIdeaCommentResponse();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }
    }
}

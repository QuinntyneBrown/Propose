using MediatR;
using Propose.Data;
using Propose.Features.Core;
using Propose.Data.Model;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;

namespace Propose.Features.Ideas
{
    public class VoteIdeaCommand
    {
        public class VoteIdeaRequest : IRequest<VoteIdeaResponse>
        {
            public int? TenantId { get; set; }
            public int? UserId { get; set; }
            public int Id { get; set; }
        }

        public class VoteIdeaResponse { }

        public class VoteIdeaHandler : IAsyncRequestHandler<VoteIdeaRequest, VoteIdeaResponse>
        {
            public VoteIdeaHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<VoteIdeaResponse> Handle(VoteIdeaRequest request)
            {                
                var idea = await _context.Ideas
                    .Include(x => x.User)
                    .Include(x=>x.Votes)
                    .SingleAsync(x=>x.Id == request.Id);

                var existingVote = idea.Votes.SingleOrDefault(x => x.UserId == request.UserId);

                if (existingVote != null)
                {
                    idea.Votes.Remove(existingVote);
                } else {
                    idea.Votes.Add(new Vote()
                    {
                        UserId = request.UserId,
                        TenantId = request.TenantId,
                        IdeaId = idea.Id
                    });
                }

                await _context.SaveChangesAsync();

                return new VoteIdeaResponse();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }
    }
}
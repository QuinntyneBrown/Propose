using MediatR;
using Propose.Data;
using Propose.Data.Model;
using Propose.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Propose.Features.Ideas
{
    public class AddOrUpdateIdeaCommand
    {
        public class AddOrUpdateIdeaRequest : IRequest<AddOrUpdateIdeaResponse>
        {
            public IdeaApiModel Idea { get; set; }
            public int? TenantId { get; set; }
            public int? UserId { get; set; }
        }

        public class AddOrUpdateIdeaResponse { }

        public class AddOrUpdateIdeaHandler : IAsyncRequestHandler<AddOrUpdateIdeaRequest, AddOrUpdateIdeaResponse>
        {
            public AddOrUpdateIdeaHandler(ProposeContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateIdeaResponse> Handle(AddOrUpdateIdeaRequest request)
            {
                var entity = await _context.Ideas
                    .Include( x => x.IdeaDigitalAssets)
                    .Include( x => x.IdeaLinks)
                    .SingleOrDefaultAsync(x => x.Id == request.Idea.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Ideas.Add(entity = new Idea());
                
                entity.TenantId = request.TenantId;

                entity.Name = request.Idea.Name;

                entity.IdeationId = request.Idea.IdeationId;

                entity.HtmlBody = request.Idea.HtmlBody;

                entity.HtmlDescription = request.Idea.HtmlDescription;

                entity.UserId = request.UserId;

                entity.IdeaDigitalAssets.Clear();

                foreach(var digitalAsset in request.Idea.DigitalAssets)
                {
                    IdeaDigitalAsset newDigitalAsset = await _context.IdeaDigitalAssets.FindAsync(digitalAsset.Id);

                    if (newDigitalAsset == null) { newDigitalAsset = new IdeaDigitalAsset(); }

                    newDigitalAsset.DigitalAssetUrl = digitalAsset.DigitalAssetUrl;

                    entity.IdeaDigitalAssets.Add(newDigitalAsset);

                }

                entity.IdeaLinks.Clear();

                foreach (var link in request.Idea.Links)
                {
                    IdeaLink newLink = await _context.IdeaLinks.FindAsync(link.Id);

                    if (newLink == null) { newLink = new IdeaLink(); }

                    entity.IdeaLinks.Add(newLink);

                }

                await _context.SaveChangesAsync();

                return new AddOrUpdateIdeaResponse();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
        }
    }
}
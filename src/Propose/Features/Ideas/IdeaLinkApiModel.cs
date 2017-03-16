using Propose.Data.Model;

namespace Propose.Features.Ideas
{
    public class IdeaLinkApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public static TModel FromIdeaLink<TModel>(IdeaLink ideaLink) where
            TModel : IdeaLinkApiModel, new()
        {
            var model = new TModel();
            model.Id = ideaLink.Id;
            model.TenantId = ideaLink.TenantId;
            model.Name = ideaLink.Name;
            model.Url = ideaLink.Url;
            return model;
        }

        public static IdeaLinkApiModel FromIdeaLink(IdeaLink ideaLink)
            => FromIdeaLink<IdeaLinkApiModel>(ideaLink);
    }
}

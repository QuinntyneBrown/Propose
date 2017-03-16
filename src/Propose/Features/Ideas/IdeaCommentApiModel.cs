using Propose.Data.Model;

namespace Propose.Features.Ideas
{
    public class IdeaCommentApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public int? IdeaId { get; set; }
        public string HtmlBody { get; set; }

        public static TModel FromIdeaComment<TModel>(IdeaComment ideaComment) where
            TModel : IdeaCommentApiModel, new()
        {
            var model = new TModel();
            model.Id = ideaComment.Id;
            model.TenantId = ideaComment.TenantId;
            model.HtmlBody = ideaComment.HtmlBody;
            model.IdeaId = ideaComment.IdeaId;
            return model;
        }

        public static IdeaCommentApiModel FromIdeaComment(IdeaComment ideaComment)
            => FromIdeaComment<IdeaCommentApiModel>(ideaComment);

    }
}

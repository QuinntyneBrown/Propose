using Propose.Data.Model;
using Propose.Features.Ideations;
using Propose.Features.Users;
using System.Collections.Generic;
using System.Linq;

namespace Propose.Features.Ideas
{
    public class IdeaApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public int? UserId { get; set; }
        public int? IdeationId { get; set; }
        public string Name { get; set; }
        public string HtmlBody { get; set; }
        public string HtmlDescription { get; set; }
        public ICollection<VoteApiModel> Votes { get; set; } = new HashSet<VoteApiModel>();
        public IdeationApiModel Ideation { get; set; }

        public UserApiModel User { get; set; }
        public static TModel FromIdea<TModel>(Idea idea) where
            TModel : IdeaApiModel, new()
        {
            var model = new TModel();
            model.Id = idea.Id;
            model.IdeationId = idea.IdeationId;
            model.TenantId = idea.TenantId;
            model.Name = idea.Name;
            model.HtmlBody = idea.HtmlBody;
            model.HtmlDescription = idea.HtmlDescription;
            model.UserId = idea.UserId;
            model.Ideation = idea.Ideation == null ? null : IdeationApiModel.FromIdeation(idea.Ideation);
            model.User = idea.User == null ? null : UserApiModel.FromUser(idea.User);
            model.Votes = idea.Votes.Select(x => VoteApiModel.FromVote(x)).ToList();
            return model;
        }

        public static IdeaApiModel FromIdea(Idea idea)
            => FromIdea<IdeaApiModel>(idea);

    }
}

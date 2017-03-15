using Propose.Data.Model;

namespace Propose.Features.Ideas
{
    public class VoteApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }

        public static TModel FromVote<TModel>(Vote vote) where
            TModel : VoteApiModel, new()
        {
            var model = new TModel();
            model.Id = vote.Id;
            model.TenantId = vote.TenantId;
            model.UserId = vote.UserId;
            return model;
        }

        public static VoteApiModel FromVote(Vote vote)
            => FromVote<VoteApiModel>(vote);
    }
}

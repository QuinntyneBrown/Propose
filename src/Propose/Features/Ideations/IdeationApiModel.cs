using Propose.Data.Model;
using System;

namespace Propose.Features.Ideations
{
    public class IdeationApiModel
    {        
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string Name { get; set; }

        public DateTime? Start { get; set; }

        public DateTime End { get; set; }

        public int? MaxiumumVotesPerUser { get; set; }

        public int? MaxiumIdeasPerUser { get; set; }

        public static TModel FromIdeation<TModel>(Ideation ideation) where
            TModel : IdeationApiModel, new()
        {
            var model = new TModel();
            model.Id = ideation.Id;
            model.TenantId = ideation.TenantId;
            model.Name = ideation.Name;
            model.Start = ideation.Start;
            model.End = ideation.End;
            model.MaxiumIdeasPerUser = ideation.MaxiumIdeasPerUser;
            model.MaxiumumVotesPerUser = ideation.MaxiumumVotesPerUser;
            return model;
        }

        public static IdeationApiModel FromIdeation(Ideation ideation)
            => FromIdeation<IdeationApiModel>(ideation);

    }
}

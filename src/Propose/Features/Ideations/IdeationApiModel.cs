using Propose.Data.Model;

namespace Propose.Features.Ideations
{
    public class IdeationApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromIdeation<TModel>(Ideation ideation) where
            TModel : IdeationApiModel, new()
        {
            var model = new TModel();
            model.Id = ideation.Id;
            model.TenantId = ideation.TenantId;
            model.Name = ideation.Name;
            return model;
        }

        public static IdeationApiModel FromIdeation(Ideation ideation)
            => FromIdeation<IdeationApiModel>(ideation);

    }
}

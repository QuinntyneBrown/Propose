using Propose.Data.Model;

namespace Propose.Features.Ideas
{
    public class IdeaDigitalAssetApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string DigitalAssetUrl { get; set; }

        public static TModel FromIdeaDigitalAsset<TModel>(IdeaDigitalAsset ideaDigitalAsset) where
            TModel : IdeaDigitalAssetApiModel, new()
        {
            var model = new TModel();
            model.Id = ideaDigitalAsset.Id;
            model.TenantId = ideaDigitalAsset.TenantId;
            model.DigitalAssetUrl = ideaDigitalAsset.DigitalAssetUrl;
            return model;
        }

        public static IdeaDigitalAssetApiModel FromIdeaDigitalAsset(IdeaDigitalAsset ideaDigitalAsset)
            => FromIdeaDigitalAsset<IdeaDigitalAssetApiModel>(ideaDigitalAsset);

    }
}

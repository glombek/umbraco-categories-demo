using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Web;
using Umbraco.Community.Contentment.DataEditors;
using Umbraco.Extensions;

namespace UmbracoCategoriesDemo.Contentment.DataSources
{
    public abstract class UmbracoContextDataListSource : IDataListSource
    {
        private readonly IRequestAccessor _requestAccessor;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public UmbracoContextDataListSource(IRequestAccessor requestAccessor, IUmbracoContextAccessor umbracoContextAccessor)
        {
            _requestAccessor = requestAccessor;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public abstract Dictionary<string, object> DefaultValues { get; }

        public abstract IEnumerable<ConfigurationField> Fields { get; }

        public virtual string Group { get; } = "Umbraco";

        public abstract OverlaySize OverlaySize { get; }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract string Icon { get; }

        public abstract IEnumerable<DataListItem> GetItems(UmbracoContext context, Dictionary<string, object> config);

        public IEnumerable<DataListItem> GetItems(Dictionary<string, object> config)
        {
            var ctx = new UmbracoContext();

            var nodeContextId = default(int?);

            var preview = true;

            var umbracoContext = _umbracoContextAccessor.GetRequiredUmbracoContext();

            if (int.TryParse(_requestAccessor.GetQueryStringValue("id"), out var currentId) == true)
            {
                nodeContextId = currentId;
            }
            else if (int.TryParse(_requestAccessor.GetQueryStringValue("parentId"), out var parentId) == true)
            {
                nodeContextId = parentId;
            }

            if (umbracoContext != null && nodeContextId.HasValue)
            {
                ctx.Content = umbracoContext.Content.GetById(preview, nodeContextId.Value);
            }

            return GetItems(ctx, config);
        }

        public class UmbracoContext
        {
            public IPublishedContent? Content { get; set; }
        }
    }
}
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Web;

namespace Umbraco.Community.Contentment.DataEditors
{
    public abstract class BaseUmbracoContentDataListSource : IDataListSource
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        private readonly IRequestAccessor _requestAccessor;

        public BaseUmbracoContentDataListSource(
            IRequestAccessor requestAccessor,
            IUmbracoContextAccessor umbracoContextAccessor)
        {
            _requestAccessor = requestAccessor;
            _umbracoContextAccessor = umbracoContextAccessor;

        }
        
        protected int? ContextContentId
        {
            get
            {
                var umbracoContext = _umbracoContextAccessor.GetRequiredUmbracoContext();
                isContextContentParent = false;

                // NOTE: First we check for "id" (if on a content page), then "parentId" (if editing an element).
                if (int.TryParse(_requestAccessor.GetQueryStringValue("id"), out var currentId) == true)
                {
                    return currentId;
                }
                else if (int.TryParse(_requestAccessor.GetQueryStringValue("parentId"), out var parentId) == true)
                {
                    isContextContentParent = true;
                    return parentId;
                }

                return default(int?);
            }
        }

        protected IPublishedContent? GetContextContent()
        {
            var umbracoContext = _umbracoContextAccessor.GetRequiredUmbracoContext();
            if (ContextContentId.HasValue == false || umbracoContext == null)
            {
                return null;
            }

            return umbracoContext?.Content?.GetById(true, ContextContentId.Value);
        }

        private bool isContextContentParent;
        protected bool IsContextContentParent
        {
            get
            {
                return ContextContentId.HasValue && isContextContentParent;
            }
        }


        public abstract Dictionary<string, object> DefaultValues { get; }

        public abstract IEnumerable<ConfigurationField> Fields { get; }

        public virtual string Group { get; } = "Umbraco";

        public abstract OverlaySize OverlaySize { get; }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract string Icon { get; }

        public abstract IEnumerable<DataListItem> GetItems(Dictionary<string, object> config);
    }
}

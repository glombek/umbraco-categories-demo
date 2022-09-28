using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Community.Contentment.DataEditors;

namespace UmbracoCategoriesDemo.Contentment.DataSources
{
    public class BlogCategoriesBlockListDataSource : UmbracoContextDataListSource
    {
        public BlogCategoriesBlockListDataSource(IRequestAccessor requestAccessor, IUmbracoContextAccessor umbracoContextAccessor) : base(requestAccessor, umbracoContextAccessor) { }

        public override Dictionary<string, object> DefaultValues => new Dictionary<string, object>();

        public override IEnumerable<ConfigurationField> Fields => new ConfigurationField[] { };

        public override OverlaySize OverlaySize => OverlaySize.Small;

        public override string Name => "Blog categories (block list)";

        public override string Description => "Pulls blog categories from the block list on the parent node.";

        public override string Icon => "icon-tags";

        public override IEnumerable<DataListItem> GetItems(UmbracoContext context, Dictionary<string, object> config)
        {
            var parent = context?.Content?.Parent as Blog;

            return parent?.CategoriesAsBlockListItems?.Cast<BlockListItem<CategoryAsBlockListItem>>().Select(x => new DataListItem() { Name = x.Content.CategoryName, Value = x.ContentUdi.ToString(), Description = x.Content.Description }) ?? new DataListItem[] { };
        }
    }
}

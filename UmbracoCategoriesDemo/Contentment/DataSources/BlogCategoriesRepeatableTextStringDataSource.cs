using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Community.Contentment.DataEditors;

namespace UmbracoCategoriesDemo.Contentment.DataSources
{
public class BlogCategoriesRepeatableTextStringDataSource : BaseUmbracoContentDataListSource
{
    public BlogCategoriesRepeatableTextStringDataSource(IRequestAccessor requestAccessor, IUmbracoContextAccessor umbracoContextAccessor) : base(requestAccessor, umbracoContextAccessor) { }

    public override Dictionary<string, object> DefaultValues => new Dictionary<string, object>();

    public override IEnumerable<ConfigurationField> Fields => new ConfigurationField[] { };

    public override OverlaySize OverlaySize => OverlaySize.Small;

    public override string Name => "Blog categories (repeatable text strings)";

    public override string Description => "Pulls blog categories from the repeatable text strings on the parent node.";

    public override string Icon => "icon-tags";

    public override IEnumerable<DataListItem> GetItems(Dictionary<string, object> config)
    {
        var parent = GetContextContent().AncestorOrSelf<Blog>();

        return parent?.CategoriesAsRepeatableTextStrings?.Select(x => new DataListItem() { Name = x, Value = x }) ?? new DataListItem[] { };
    }
}
}

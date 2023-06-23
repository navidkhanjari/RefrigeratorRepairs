using RefrigeratorRepairs.UI.ViewModels.Paging;

namespace RefrigeratorRepairs.UI.ViewModels.Articles
{
    public class ArticleFilterViewModel : BasePaging<MODEL.Entities.Article.Article>
    {
        #region (Fields)
        public string Title { get; set; }
        #endregion
    }
}

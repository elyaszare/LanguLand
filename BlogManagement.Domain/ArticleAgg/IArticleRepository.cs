using System.Collections.Generic;
using _0_Framework.Domain;
using BlogManagement.Application.Contracts.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<long, Article>
    {
        EditArticle GetDetails(long id);
        List<ArticleViewModel> GetArticles();
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}

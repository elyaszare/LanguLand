using _0_Framework;
using AccountManagement.Domain.AccountAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

//using AccountManagement.Domain.AccountAgg;

namespace BlogManagement.Domain.ArticleAgg
{
    public class Article : EntityBase
    {
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public long AuthorId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public long CategoryId { get; set; }
        public bool IsRemoved { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public ArticleCategory ArticleCategory { get; set; }
        public Account Author { get; set; }

        public Article(string title, string pageTitle, long authorId, string shortDescription, string description,
            string picture, string pictureAlt,
            string pictureTitle, long categoryId, string keywords, string metaDescription, string slug)
        {
            Title = title;
            PageTitle = pageTitle;
            AuthorId = authorId;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            CategoryId = categoryId;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Edit(string title, string pageTitle, long authorId, string shortDescription, string description,
            string picture, string pictureAlt,
            string pictureTitle, long categoryId, string keywords, string metaDescription, string slug)
        {
            Title = title;
            PageTitle = pageTitle;
            AuthorId = authorId;
            ShortDescription = shortDescription;
            Description = description;

            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            CategoryId = categoryId;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void ReStore()
        {
            IsRemoved = false;
        }
    }
}

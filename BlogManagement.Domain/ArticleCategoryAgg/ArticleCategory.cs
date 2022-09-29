using System.Collections.Generic;
using _0_Framework;
using BlogManagement.Domain.ArticleAgg;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public bool IsRemoved { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }

        public List<Article> Articles { get; set; }

        public ArticleCategory(string name, string description, string picture, string pictureAlt, string pictureTitle,
            string keywords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            IsRemoved = false;
        }

        public void Edit(string name, string description, string picture, string pictureAlt, string pictureTitle,
            string keywords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;

            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            IsRemoved = false;
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

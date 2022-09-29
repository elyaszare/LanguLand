using System.Collections.Generic;
using _0_Framework;
using AccountManagement.Domain.AccountAgg;
using CourseManagement.Domain.CourseCategoryAgg;

namespace CourseManagement.Domain.CourseAgg
{
    public class Course:EntityBase
    {
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public long TeacherId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public int Star { get; set; }
        public string Level { get; set; }
        public string Time { get; set; }
        public double Price { get; set; }
        public long CategoryId { get; set; }
        public bool IsRemoved { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public Account Teacher { get; set; }
        public List<Account> Students { get; set; }
        public CourseCategory CourseCategory { get; set; }

        public Course(string title, string pageTitle, long teacherId, string shortDescription, string description,
            string picture,
            string pictureAlt,
            string pictureTitle, string level, string time, double price, long categoryId, string keywords,
            string metaDescription, string slug)
        {
            Title = title;
            PageTitle = pageTitle;
            TeacherId = teacherId;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Star = 0;
            Level = level;
            Time = time;
            Price = price;
            CategoryId = categoryId;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            IsRemoved = false;
        }

        public void Edit(string title, string pageTitle, long teacherId, string shortDescription, string description,
            string picture,
            string pictureAlt,
            string pictureTitle, string level, string time, double price, long categoryId, string keywords,
            string metaDescription, string slug)
        {
            Title = title;
            PageTitle = pageTitle;
            TeacherId = teacherId;
            ShortDescription = shortDescription;
            Description = description;

            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Level = level;
            Time = time;
            Price = price;
            CategoryId = categoryId;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}

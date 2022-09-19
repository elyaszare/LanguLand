using System.Collections.Generic;
using _0_Framework;
using CourseManagement.Domain.CourseAgg;

namespace CourseManagement.Domain.CourseCategoryAgg
{
    public class CourseCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string MetaDescription { get; private set; }
        public string Keywords { get; private set; }
        public string Slug { get; private set; }
        public bool IsRemoved { get; private set; }
        public List<Course> Courses { get; set; }
        public CourseCategory(string name, string description, string picture, string pictureAlt, string pictureTitle, string metaDescription, string keywords, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            MetaDescription = metaDescription;
            Keywords = keywords;
            Slug = slug;
            IsRemoved = false;
        }

        public void Edit(string name, string description, string picture, string pictureAlt, string pictureTitle, string metaDescription, string keywords, string slug)
        {
            Name = name;
            Description = description;

            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            MetaDescription = metaDescription;
            Keywords = keywords;
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

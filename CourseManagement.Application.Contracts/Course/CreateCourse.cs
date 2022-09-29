using System.Collections.Generic;
using CourseManagement.Application.Contracts.CourseCategory;
using Microsoft.AspNetCore.Http;

namespace CourseManagement.Application.Contracts.Course
{
    public class CreateCourse
    {
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public long TeacherId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Level { get; set; }
        public string Time { get; set; }
        public double Price { get; set; }
        public long CategoryId { get; set; }
        public bool IsRemoved { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public List<CourseCategoryViewModel> CourseCategories { get; set; }
    }
}

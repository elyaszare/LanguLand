using Microsoft.AspNetCore.Http;

namespace CourseManagement.Application.Contracts.CourseCategory

{
public class CreateCourseCategory
{
    public string Name { get; set; }
    public string Description { get; set; }

    public IFormFile Picture { get; set; }
    public string PictureAlt { get; set; }
    public string PictureTitle { get; set; }

    public string Keywords { get; set; }

    public string MetaDescription { get; set; }

    public string Slug { get; set; }
}
}
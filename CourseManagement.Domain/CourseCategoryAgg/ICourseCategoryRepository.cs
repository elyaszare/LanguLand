using System.Collections.Generic;
using _0_Framework.Domain;
using CourseManagement.Application.Contracts.CourseCategory;

namespace CourseManagement.Domain.CourseCategoryAgg
{
    public interface ICourseCategoryRepository : IRepository<long, CourseCategory>
    {
        EditCourseCategory GetDetails(long id);
        List<CourseCategoryViewModel> GetCourseCategories();
        List<CourseCategoryViewModel> Search(CourseCategorySearchModel searchModel);
    }
}

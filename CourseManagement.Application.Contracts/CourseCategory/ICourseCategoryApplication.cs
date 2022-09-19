using System.Collections.Generic;
using _0_Framework.Application;

namespace CourseManagement.Application.Contracts.CourseCategory
{
    public interface ICourseCategoryApplication
    {
        OperationResult Create(CreateCourseCategory command);
        OperationResult Edit(EditCourseCategory command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditCourseCategory GetDetails(long id);
        List<CourseCategoryViewModel> GetCourseCategories();
        List<CourseCategoryViewModel> Search(CourseCategorySearchModel searchModel);
    }
}
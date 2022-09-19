using System.Collections.Generic;
using _0_Framework.Application;

namespace CourseManagement.Application.Contracts.Course
{
    public interface ICourseApplication
    {
        OperationResult Create(CreateCourse command);
        OperationResult Edit(EditCourse command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditCourse GetDetails(long id);
        List<CourseViewModel> GetCourses();
        List<CourseViewModel> Search(CourseSearchModel searchModel);
    }
}

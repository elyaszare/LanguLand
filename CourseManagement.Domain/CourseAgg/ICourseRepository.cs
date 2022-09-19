using System.Collections.Generic;
using _0_Framework.Domain;
using CourseManagement.Application.Contracts.Course;

namespace CourseManagement.Domain.CourseAgg
{
    public interface ICourseRepository : IRepository<long, Course>
    {
        EditCourse GetDetails(long id);
        List<CourseViewModel> GetCourses();
        List<CourseViewModel> Search(CourseSearchModel searchModel);
    }
}

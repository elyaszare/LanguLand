
using CourseManagement.Application;
using CourseManagement.Application.Contracts.Course;
using CourseManagement.Application.Contracts.CourseCategory;
using CourseManagement.Domain.CourseAgg;
using CourseManagement.Domain.CourseCategoryAgg;
using CourseManagement.Infrastructure.EFCore;
using CourseManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseCategoryManagement.Infrastructure.Configuration
{
    public class CourseManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<ICourseCategoryApplication, CourseCategoryApplication>();
            service.AddTransient<ICourseCategoryRepository, CourseCategoryRepository>();

            service.AddTransient<ICourseApplication, CourseApplication>();
            service.AddTransient<ICourseRepository, CourseRepository>();

            service.AddDbContext<CourseContext>(x => x.UseSqlServer(connectionString));
        }
    }
}

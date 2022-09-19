using System.Collections.Generic;
using CourseManagement.Application.Contracts.Course;
using CourseManagement.Application.Contracts.CourseCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Courses.Course
{
    public class IndexModel : PageModel
    {
        public CourseSearchModel SearchModel;
        public List<CourseViewModel> Courses;
        public SelectList CourserCategories;
        public string Message { get; set; }
        private readonly ICourseApplication _courseApplication;
        private readonly ICourseCategoryApplication _courseCategoryApplication;

        public IndexModel(ICourseApplication courseApplication, ICourseCategoryApplication courseCategoryApplication)
        {
            _courseApplication = courseApplication;
            _courseCategoryApplication = courseCategoryApplication;
        }

        public void OnGet(CourseSearchModel searchModel)
        {
            CourserCategories = new SelectList(_courseCategoryApplication.GetCourseCategories(), "Id", "Name");
            Courses = _courseApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateCourse
            {
                CourseCategories = _courseCategoryApplication.GetCourseCategories()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateCourse command)
        {
            var result = _courseApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var course = _courseApplication.GetDetails(id);
            course.CourseCategories = _courseCategoryApplication.GetCourseCategories();
            return Partial("./Edit", course);
        }

        public JsonResult OnPostEdit(EditCourse command)
        {
            var result = _courseApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _courseApplication.Remove(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _courseApplication.Restore(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}

using System.Collections.Generic;
using CourseManagement.Application.Contracts.CourseCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Courses.CourseCategory
{
    public class IndexModel : PageModel
    {
        public string Message;
        public CourseCategorySearchModel SearchModel;
        public List<CourseCategoryViewModel> CourseCategories;
        private readonly ICourseCategoryApplication _courseCategoryApplication;

        public IndexModel(ICourseCategoryApplication courseCategoryApplication)
        {
            _courseCategoryApplication = courseCategoryApplication;
        }

        public void OnGet(CourseCategorySearchModel searchModel)
        {
            CourseCategories = _courseCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateCourseCategory());
        }

        public JsonResult OnPostCreate(CreateCourseCategory command)
        {
            var result = _courseCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var courseCategory = _courseCategoryApplication.GetDetails(id);
            return Partial("./Edit", courseCategory);
        }

        public JsonResult OnPostEdit(EditCourseCategory command)
        {
            var result = _courseCategoryApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _courseCategoryApplication.Remove(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _courseCategoryApplication.Restore(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}

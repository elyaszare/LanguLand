using System.Collections.Generic;
using CourseManagement.Application.Contracts.Course;
using DiscountManagement.Application.Contracts.DiscountCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Discount.DiscountCode
{
    public class IndexModel : PageModel
    {
        public DiscountCodeSearchModel SearchModel;
        public List<DiscountCodeViewModel> DiscountCodes;
        public SelectList Courses { get; set; }

        private readonly IDiscountCodeApplication _discountCodeApplication;
        private readonly ICourseApplication _courseApplication;

        public IndexModel(IDiscountCodeApplication discountCodeApplication, ICourseApplication courseApplication)
        {
            _discountCodeApplication = discountCodeApplication;
            _courseApplication = courseApplication;
        }


        public void OnGet(DiscountCodeSearchModel searchModel)
        {
            DiscountCodes = _discountCodeApplication.Search(searchModel);
            Courses = new SelectList(_courseApplication.GetCourses(), "Id", "Title");
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineDiscountCode
            {
                Courses = _courseApplication.GetCourses()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineDiscountCode command)
        {
            var result = _discountCodeApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var discountCode = _discountCodeApplication.GetDetails(id);
            discountCode.Courses = _courseApplication.GetCourses();
            return Partial("./Edit", discountCode);
        }

        public JsonResult OnPostEdit(EditDiscountCode command)
        {
            var result = _discountCodeApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}

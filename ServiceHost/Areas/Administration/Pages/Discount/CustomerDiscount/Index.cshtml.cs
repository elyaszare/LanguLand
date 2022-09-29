using System.Collections.Generic;
using CourseManagement.Application.Contracts.Course;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public CustomerDiscountSearchModel SearchModel;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public SelectList Courses;

        private readonly ICourseApplication _courseApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;

        public IndexModel(ICustomerDiscountApplication customerDiscountApplication,
            ICourseApplication courseApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _courseApplication = courseApplication;
        }


        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Courses = new SelectList(_courseApplication.GetCourses(), "Id", "Title");
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Courses = _courseApplication.GetCourses()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount = _customerDiscountApplication.GetDetails(id);
            customerDiscount.Courses = _courseApplication.GetCourses();
            return Partial("Edit", customerDiscount);
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}

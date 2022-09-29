using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using CourseManagement.Application.Contracts.Course;

namespace DiscountManagement.Application.Contracts.DiscountCode
{
    public class DefineDiscountCode
    {
        public long CourseId { get; set; }

        public string Code { get; set; }

        [Range(1, 100000, ErrorMessage = ValidationMessage.IsRequired)]
        public int DiscountRate { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string StartDate { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string EndDate { get; set; }

        public string Reason { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}

using System;
using _0_Framework;

namespace DiscountManagement.Domain.DiscountCodeAgg
{
    public class DiscountCode : EntityBase
    {
        public long CourseId { get; private set; }
        public string Code { get; private set; }
        public int DiscountRate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Reason { get; private set; }

        public DiscountCode(long courseId, string code, int discountRate, DateTime startDate, DateTime endDate,
            string reason)
        {
            CourseId = courseId;
            Code = code;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }

        public void Edit(long courseId, string code, int discountRate, DateTime startDate, DateTime endDate,
            string reason)
        {
            CourseId = courseId;
            Code = code;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }
    }
}

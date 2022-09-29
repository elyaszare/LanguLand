using System;
using _0_Framework;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public class CustomerDiscount : EntityBase

    {
        public long CourseId { get; private set; }
        public int DiscountRate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Reason { get; private set; }

        public CustomerDiscount(long courseId, int discountRate, DateTime startDate, DateTime endDate, string reason)
        {
            CourseId = courseId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }

        public void Edit(long courseId, int discountRate, DateTime startDate, DateTime endDate, string reason)
        {
            CourseId = courseId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }
    }
}

namespace DiscountManagement.Application.Contracts.DiscountCode
{
    public class DiscountCodeSearchModel
    {
        public long CourseId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
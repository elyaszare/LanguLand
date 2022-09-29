namespace CourseManagement.Application.Contracts.Course
{
    public class CourseViewModel
    {
        public long Id { get; set; }
        public long TeacherId { get; set; }
        public string Teacher { get; set; }
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public string Picture { get; set; }
        public string Level { get; set; }
        public double Price { get; set; }
        public long CategoryId { get; set; }
        public bool IsRemoved { get; set; }
        public string CourseCategory { get; set; }
        public string CreationDate { get; set; }
    }
}
namespace CourseManagement.Application.Contracts.Course
{
    public class CourseSearchModel
    {
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public long TeacherId { get; set; }
    }
}
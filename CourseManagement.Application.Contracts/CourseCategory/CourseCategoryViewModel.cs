namespace CourseManagement.Application.Contracts.CourseCategory
{
    public class CourseCategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string CreationDate { get; set; }
        public bool IsRemoved { get; set; }
    }
}
using System.Collections.Generic;
using _0_Framework.Application;
using CourseManagement.Application.Contracts.Course;
using CourseManagement.Domain.CourseAgg;

namespace CourseManagement.Application
{
    public class CourseApplication : ICourseApplication
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IFileUploader _fileUploader;

        public CourseApplication(IFileUploader fileUploader, ICourseRepository courseRepository)
        {
            _fileUploader = fileUploader;
            _courseRepository = courseRepository;
        }

        public OperationResult Create(CreateCourse command)
        {
            var operation = new OperationResult();

            if (_courseRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var slug = command.Slug.Slugify();
            var filePath = $"Courses//{slug}";
            var fileName = _fileUploader.Upload(command.Picture, filePath);

            var course = new Course(command.Title, command.PageTitle, command.TeacherId, command.ShortDescription,
                command.Description, fileName,
                command.PictureAlt, command.PictureTitle, command.Level, command.Time, command.Price,
                command.CategoryId, command.Keywords, command.MetaDescription, slug);
            _courseRepository.Create(course);
            _courseRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditCourse command)
        {
            var operation = new OperationResult();
            var courseCategory = _courseRepository.Get(command.Id);

            if (courseCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_courseRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var slug = command.Slug.Slugify();
            var filePath = $"Courses//{slug}";
            var fileName = _fileUploader.Upload(command.Picture, filePath);

            courseCategory.Edit(command.Title, command.PageTitle, command.TeacherId, command.ShortDescription,
                command.Description, fileName,
                command.PictureAlt, command.PictureTitle, command.Level, command.Time, command.Price,
                command.CategoryId, command.Keywords, command.MetaDescription, slug);

            _courseRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var courseCategory = _courseRepository.Get(id);

            if (courseCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            courseCategory.Remove();

            _courseRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var courseCategory = _courseRepository.Get(id);

            if (courseCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            courseCategory.Restore();

            _courseRepository.SaveChanges();
            return operation.Success();
        }

        public EditCourse GetDetails(long id)
        {
            return _courseRepository.GetDetails(id);
        }

        public List<CourseViewModel> GetCourses()
        {
            return _courseRepository.GetCourses();
        }

        public List<CourseViewModel> Search(CourseSearchModel searchModel)
        {
            return _courseRepository.Search(searchModel);
        }
    }
}

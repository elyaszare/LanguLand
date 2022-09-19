using System.Collections.Generic;
using _0_Framework.Application;
using CourseManagement.Application.Contracts.CourseCategory;
using CourseManagement.Domain.CourseCategoryAgg;

namespace CourseManagement.Application
{
    public class CourseCategoryApplication : ICourseCategoryApplication
    {
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public CourseCategoryApplication(ICourseCategoryRepository courseCategoryRepository, IFileUploader fileUploader)
        {
            _courseCategoryRepository = courseCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateCourseCategory command)
        {
            var operation = new OperationResult();

            if (_courseCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var slug = command.Slug.Slugify();
            var filePath = $"{slug}";
            var fileName = _fileUploader.Upload(command.Picture, filePath);

            var courseCategory = new CourseCategory(command.Name, command.Description, fileName, command.PictureAlt,
                command.PictureTitle, command.MetaDescription, command.Keywords, slug);

            _courseCategoryRepository.Create(courseCategory);
            _courseCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditCourseCategory command)
        {
            var operation = new OperationResult();
            var courseCategory = _courseCategoryRepository.Get(command.Id);

            if (courseCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_courseCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var slug = command.Slug.Slugify();
            var filePath = $"{slug}";
            var fileName = _fileUploader.Upload(command.Picture, filePath);

            courseCategory.Edit(command.Name, command.Description, fileName, command.PictureAlt, command.PictureTitle,
                command.MetaDescription, command.Keywords, slug);

            _courseCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var courseCategory = _courseCategoryRepository.Get(id);

            if (courseCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            courseCategory.Remove();

            _courseCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var courseCategory = _courseCategoryRepository.Get(id);

            if (courseCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            courseCategory.ReStore();

            _courseCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public EditCourseCategory GetDetails(long id)
        {
            return _courseCategoryRepository.GetDetails(id);
        }

        public List<CourseCategoryViewModel> GetCourseCategories()
        {
            return _courseCategoryRepository.GetCourseCategories();
        }

        public List<CourseCategoryViewModel> Search(CourseCategorySearchModel searchModel)
        {
            return _courseCategoryRepository.Search(searchModel);
        }
    }
}

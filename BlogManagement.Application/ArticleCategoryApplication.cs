using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository,
            IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();

            if (_articleCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var slug = command.Slug.Slugify();
            var filePath = $"{slug}";
            var fileName = _fileUploader.Upload(command.Picture, filePath);

            var articleCategory = new ArticleCategory(command.Name, command.Description, fileName, command.PictureAlt,
                command.PictureTitle, command.Keywords, command.MetaDescription, slug);

            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();
            var articleCategory = _articleCategoryRepository.Get(command.Id);

            if (articleCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_articleCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var slug = command.Slug.Slugify();
            var filePath = $"{slug}";
            var fileName = _fileUploader.Upload(command.Picture, filePath);

            articleCategory.Edit(command.Name, command.Description, fileName, command.PictureAlt, command.PictureTitle,
                command.MetaDescription, command.Keywords, slug);

            _articleCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var courseCategory = _articleCategoryRepository.Get(id);

            if (courseCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            courseCategory.Remove();

            _articleCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var courseCategory = _articleCategoryRepository.Get(id);

            if (courseCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            courseCategory.ReStore();

            _articleCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _articleCategoryRepository.GetArticleCategories();
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }
    }
}

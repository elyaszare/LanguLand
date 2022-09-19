using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CourseManagement.Application.Contracts.CourseCategory;
using CourseManagement.Domain.CourseCategoryAgg;

namespace CourseManagement.Infrastructure.EFCore.Repository
{
    public class CourseCategoryRepository : RepositoryBase<long, CourseCategory>, ICourseCategoryRepository
    {
        private readonly CourseContext _context;

        public CourseCategoryRepository(CourseContext context) : base(context)
        {
            _context = context;
        }

        public EditCourseCategory GetDetails(long id)
        {
            return _context.CourseCategories.Select(x => new EditCourseCategory
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).FirstOrDefault(x => x.Id == id)!;
        }

        public List<CourseCategoryViewModel> GetCourseCategories()
        {
            return _context.CourseCategories.Select(x => new CourseCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                Description = x.Description,
                CreationDate = x.CreationDate.ToFarsi(),
                IsRemoved = x.IsRemoved
            }).OrderByDescending(x => x.Id).ToList();
        }

        public List<CourseCategoryViewModel> Search(CourseCategorySearchModel searchModel)
        {
            var query = _context.CourseCategories.Select(x => new CourseCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                Description = x.Description,
                CreationDate = x.CreationDate.ToFarsi(),
                IsRemoved = x.IsRemoved
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}

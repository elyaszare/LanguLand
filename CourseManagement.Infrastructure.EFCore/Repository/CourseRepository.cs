using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CourseManagement.Application.Contracts.Course;
using CourseManagement.Domain.CourseAgg;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Infrastructure.EFCore.Repository
{
    public class CourseRepository : RepositoryBase<long, Course>, ICourseRepository
    {
        private readonly CourseContext _context;

        public CourseRepository(CourseContext context) : base(context)
        {
            _context = context;
        }

        public EditCourse GetDetails(long id)
        {
            return _context.Courses.Select(x => new EditCourse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ShortDescription = x.ShortDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Price = x.Price,
                Level = x.Level,
                Time = x.Time,
                CategoryId = x.CategoryId,
                IsRemoved = x.IsRemoved,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CourseViewModel> GetCourses()
        {
            return _context.Courses.Include(x => x.CourseCategory).Select(x => new CourseViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Level = x.Level,
                Picture = x.Picture,
                CategoryId = x.CategoryId,
                IsRemoved = x.IsRemoved,
                CourseCategory = x.CourseCategory.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).ToList();
        }

        public List<CourseViewModel> Search(CourseSearchModel searchModel)
        {
            var query = _context.Courses.Include(x => x.CourseCategory).Select(x => new CourseViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Level = x.Level,
                Picture = x.Picture,
                CategoryId = x.CategoryId,
                IsRemoved = x.IsRemoved,
                CourseCategory = x.CourseCategory.Name,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title));

            if (searchModel.CategoryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}

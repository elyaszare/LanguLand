using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CourseManagement.Infrastructure.EFCore;
using DiscountManagement.Application.Contracts.DiscountCode;
using DiscountManagement.Domain.DiscountCodeAgg;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class DiscountCodeRepository : RepositoryBase<long, DiscountCode>, IDiscountCodeRepository
    {
        private readonly DiscountContext _context;
        private readonly CourseContext _courseContext;

        public DiscountCodeRepository(DiscountContext context, CourseContext courseContext) : base(context)
        {
            _context = context;
            _courseContext = courseContext;
        }

        public EditDiscountCode GetDetails(long id)
        {
            return _context.DiscountCodes.Select(x => new EditDiscountCode
            {
                Id = x.Id,
                Code = x.Code,
                CourseId = x.CourseId,
                StartDate = x.StartDate.ToString(),
                EndDate = x.EndDate.ToString(),
                Reason = x.Reason,
                DiscountRate = x.DiscountRate
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<DiscountCodeViewModel> Search(DiscountCodeSearchModel searchModel)
        {
            var courses = _courseContext.Courses.Select(x => new {x.Id, x.Title}).ToList();

            var query = _context.DiscountCodes.Select(x => new DiscountCodeViewModel
            {
                Id = x.Id,
                Code = x.Code,
                CourseId = x.CourseId,
                EndDate = x.EndDate.ToFarsi(),
                StartDate = x.StartDate.ToFarsi(),
                CreationDate = x.CreationDate.ToFarsi(),
                Reason = x.Reason,
                DiscountRate = x.DiscountRate,
                EndDateGr = x.EndDate,
                StartDateGr = x.StartDate
            });
            if (searchModel.CourseId > 0)
                query = query.Where(x => x.CourseId == searchModel.CourseId);

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
                query = query.Where(x => x.StartDateGr > searchModel.StartDate.ToGeorgianDateTime());

            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
                query = query.Where(x => x.EndDateGr < searchModel.EndDate.ToGeorgianDateTime());

            var discounts = query.OrderByDescending(x => x.Id).ToList();

            discounts.ForEach(discount =>
                discount.Course = courses.FirstOrDefault(x => x.Id == discount.CourseId)?.Title);

            return discounts;
        }
    }
}

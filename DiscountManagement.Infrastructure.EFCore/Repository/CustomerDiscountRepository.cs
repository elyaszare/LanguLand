using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CourseManagement.Infrastructure.EFCore;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly CourseContext _courseContext;

        public CustomerDiscountRepository(DiscountContext context, CourseContext courseContext) : base(context)
        {
            _context = context;
            _courseContext = courseContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount
            {
                Id = x.Id,
                CourseId = x.CourseId,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToString(),
                StartDate = x.StartDate.ToString(),
                Reason = x.Reason
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var courses = _courseContext.Courses.Select(x => new {x.Id, x.Title}).ToList();

            var query = _context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                CourseId = x.CourseId,
                Reason = x.Reason,
                EndDate = x.EndDate.ToFarsi(),
                StartDate = x.StartDate.ToFarsi(),
                CreationDate = x.CreationDate.ToFarsi(),
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

using System.Collections.Generic;
using _0_Framework.Domain;
using DiscountManagement.Application.Contracts.DiscountCode;

namespace DiscountManagement.Domain.DiscountCodeAgg
{
    public interface IDiscountCodeRepository : IRepository<long, DiscountCode>
    {
        EditDiscountCode GetDetails(long id);
        List<DiscountCodeViewModel> Search(DiscountCodeSearchModel searchModel);
    }
}

using System.Collections.Generic;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.DiscountCode
{
    public interface IDiscountCodeApplication
    {
        OperationResult Define(DefineDiscountCode command);
        OperationResult Edit(EditDiscountCode command);
        EditDiscountCode GetDetails(long id);
        List<DiscountCodeViewModel> Search(DiscountCodeSearchModel searchModel);
    }
}

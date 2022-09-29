using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.DiscountCode;
using DiscountManagement.Domain.DiscountCodeAgg;

namespace DiscountManagement.Application
{
    public class DiscountCodeApplication : IDiscountCodeApplication
    {
        private readonly IDiscountCodeRepository _discountCodeRepository;

        public DiscountCodeApplication(IDiscountCodeRepository discountCodeRepository)
        {
            _discountCodeRepository = discountCodeRepository;
        }

        public OperationResult Define(DefineDiscountCode command)
        {
            var operation = new OperationResult();
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            var customerDiscount =
                new DiscountCode(command.CourseId, command.Code, command.DiscountRate, startDate, endDate,
                    command.Reason);
            _discountCodeRepository.Create(customerDiscount);
            _discountCodeRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditDiscountCode command)
        {
            var operation = new OperationResult();
            var customerDiscount = _discountCodeRepository.Get(command.Id);
            if (customerDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);


            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            customerDiscount.Edit(command.CourseId, command.Code, command.DiscountRate, startDate, endDate,
                command.Reason);
            _discountCodeRepository.SaveChanges();
            return operation.Success();
        }

        public EditDiscountCode GetDetails(long id)
        {
            return _discountCodeRepository.GetDetails(id);
        }

        public List<DiscountCodeViewModel> Search(DiscountCodeSearchModel searchModel)
        {
            return _discountCodeRepository.Search(searchModel);
        }
    }
}

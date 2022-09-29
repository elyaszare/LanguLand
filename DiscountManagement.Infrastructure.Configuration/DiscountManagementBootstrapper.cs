using DiscountManagement.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Application.Contracts.DiscountCode;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Domain.DiscountCodeAgg;
using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Infrastructure.Configuration
{
    public class DiscountManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            service.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

            service.AddTransient<IDiscountCodeApplication, DiscountCodeApplication>();
            service.AddTransient<IDiscountCodeRepository, DiscountCodeRepository>();

            service.AddDbContext<DiscountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}

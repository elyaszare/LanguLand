using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public Account GetBy(string email)
        {
            return _context.Accounts.Where(x => x.IsActive).FirstOrDefault(x => x.Email == email);
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                Mobile = x.Mobile,
                Password = x.Password,
                ActiveCode = x.ActiveCode,
                Email = x.Email
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                Mobile = x.Mobile,
                ProfilePhoto = x.ProfilePhoto,
                IsActive = x.IsActive,
                Email = x.Email,
                CreationDate = x.CreationDate.ToFarsi()
            });


            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
                query = query.Where(x => x.Fullname.Contains(searchModel.FullName));

            if (!string.IsNullOrWhiteSpace(searchModel.Username))
                query = query.Where(x => x.Username.Contains(searchModel.Username));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));

            //if (searchModel.RoleId > 0)
            //    query = query.Where(x => x.RoleId == searchModel.RoleId);


            return query.OrderByDescending(x => x.Id).ToList();
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname
            }).ToList();
        }

        public Account GetByCode(string activeCode)
        {
            return _context.Accounts.FirstOrDefault(x => x.ActiveCode == activeCode);
        }
    }
}

using System.Collections.Generic;
using _0_Framework.Application;
using _0_Framework.Application.Email;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IFileUploader _fileUploader;
        private readonly IAuthHelper _authHelper;
        private readonly IEmailService _emailService;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher,
            IFileUploader fileUploader, IAuthHelper authHelper, IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
            _emailService = emailService;
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();
            var accountBy = _accountRepository.GetBy(command.Email);

            if (_accountRepository.Exists(x => x.Email == command.Email || x.Mobile == command.Mobile))
                return operation.Failed("حساب کاربری با این مشخصات وجود دارد . می توانید وارد حساب کاربری خود شوید");


            if (accountBy?.ActiveCode != null)
                return operation.Success("کاربر گرامی کد فعال سازی حساب کاربری  به ایمیل شما ارسال شده است.");

            var password = _passwordHasher.Hash(command.Password);

            var path = $"ProfilePhotos//{command.Fullname}";

            var fileName = _fileUploader.Upload(command.ProfilePhoto, path);

            var activeCode = AccountCodeGenerator.Generate();

            var account = new Account(command.Fullname, command.Username, command.Email, command.Mobile, password,
                fileName, activeCode);

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            _emailService.SendEmail(command.Email, "ثبت نام موفق!",
                $"سلام و عرض ادب خدمت شما کاربر عزیز سایت دیجی آجیلی :) \n به سایت دیجی آجیلی خوش آمدید \n باعث افتخار ماست که برای خرید آسان و مطمئن دیجی آجیلی را انتخاب کرده اید... \n جهت فعال سازی حساب کاربری خود از لینک {"https://digiajili.ir/ActiveAccount/" + activeCode} وارد شوید \n باتشکر، مدیریت دیجی آجیلی");
            return operation.Success(
                "کاربر گرامی \n لینک فعالسازی حساب کربری به ایمیل شما ارسال شده است جهت فعالسازی وارد لینک ارسالی شوید و سپس وارد حساب کاربری خود شوید.  ");
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operation.Failed("حساب کاربری با این اطلاعات یافت نشد");

            if (_accountRepository.Exists(x =>
                (x.Username == command.Username || x.Mobile == command.Mobile) && x.Id != command.Id))
                return operation.Failed("جساب کاربری با این مشخصات وجود دارد. لطفا وارد شوید");

            var path = $"ProfilePhotos//{command.Fullname}";

            var fileName = _fileUploader.Upload(command.ProfilePhoto, path);
            account.Edit(command.Fullname, command.Username, command.Email, command.Mobile, fileName,
                command.ActiveCode);
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordNotMatch);

            //Hashing Password
            var password = _passwordHasher.Hash(command.Password);

            account.ChangePassword(password);
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Email);

            if (account == null)
                return operation.Failed("حساب کاربری با این اطلاعات وجود ندارد، لطفا ثبت نام کنید.");

            var passwordResult = _passwordHasher.Check(account.Password, command.Password);

            if (!passwordResult.Verified)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            var authViewModel = new AuthViewModel(account.Id, 1, account.Fullname, account.Username, account.Mobile,
                account.Email, new List<int>());
            _authHelper.Signin(authViewModel);
            return operation.Success();
        }

        public OperationResult Active(string activeCode)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetByCode(activeCode);

            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (account.IsActive)
                return operation.Success("حساب کاربری شما فعال است");

            account.Active();
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Active(long id)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(id);

            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (account.IsActive)
                return operation.Success("حساب کاربری شما فعال است");

            account.Active();
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }
    }
}

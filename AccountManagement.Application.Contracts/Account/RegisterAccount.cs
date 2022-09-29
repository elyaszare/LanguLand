using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account
{
    public class RegisterAccount
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public IFormFile ProfilePhoto { get; set; }
        public string ActiveCode { get; set; }
    }
}
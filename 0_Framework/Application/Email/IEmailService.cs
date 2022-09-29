namespace _0_Framework.Application.Email
{
    public interface IEmailService
    {
        string SendEmail(string toAddress, string subject, string body);
    }
}
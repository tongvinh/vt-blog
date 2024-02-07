namespace VTBlog.WebApp.Services
{
    public interface IEmailSender
    {
        Task SendEmail (EmailData emailData);
    }
}

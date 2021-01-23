using BdhCMS.Web.Models;
namespace BdhCMS.Web.ServicesMail
{
    public interface ISmtpService
    {
        bool SendEmail(ContactFormViewModel model);
    }
}

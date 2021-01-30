using BdhCMS.Web.Models;
namespace BdhCMS.Web.ServicesMail
{
    public interface ISmtpService
    {
        string SendContactToEmail(ContactFormViewModel model, string bodyEmail);
        string SendApplyJobEmail(ApplyJobFormViewModel model, string bodyEmail);
    }
}

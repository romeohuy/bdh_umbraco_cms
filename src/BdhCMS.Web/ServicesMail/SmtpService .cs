using System;
using System.Net.Mail;
using Umbraco.Core.Logging;
using System.Net;
using BdhCMS.Web.ServicesMail;
using BdhCMS.Web.Models;
using BdhCMS.Web.Controllers;
using System.Configuration;

namespace BdhCMS.Web.ServicesMail
{
    public class SmtpService : ISmtpService
    {
        private readonly ILogger _logger;

        public SmtpService(ILogger logger)
        {
            _logger = logger;
        }

        public bool SendEmail(ContactFormViewModel model)
        {
            try
            {
                var frommail = new MailAddress(ConfigurationManager.AppSettings["MailAddress"]);
                var Pass = ConfigurationManager.AppSettings["EmailPassword"];
                var tomail = new MailAddress(ConfigurationManager.AppSettings["EmailReceive"]);
                string Title = ConfigurationManager.AppSettings["EmailTitle"];
                string Message = " Name: " + model.Name + " Phone: " + model.Phone + " \n\nEmail: " + model.Email + " \n\nMessages: " + model.Message;
                var smtp = new SmtpClient()
                {
                    Host = ConfigurationManager.AppSettings["HostMail"],
                    Port = int.Parse(ConfigurationManager.AppSettings["PortMail"]),
                    EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSslMail"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = bool.Parse(ConfigurationManager.AppSettings["UseDefaultCredentialsMail"]),
                    Credentials = new NetworkCredential(frommail.Address, Pass)
                };
                var mess = new MailMessage(frommail, tomail)
                {
                    Subject = Title,
                    Body = Message,
                };
                smtp.Send(mess);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(ContactFormController), ex, "Error sending contact form.");
                return false;
            }
        }
    }
}

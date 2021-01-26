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

        public string SendContactToEmail(ContactFormViewModel model)
        {
            try
            {
                var fromEmail = new MailAddress(ConfigurationManager.AppSettings["MailAddress"]);
                var pass = ConfigurationManager.AppSettings["EmailPassword"];
                var toEmail = new MailAddress(ConfigurationManager.AppSettings["EmailReceive"]);
                string title = ConfigurationManager.AppSettings["EmailTitle"];

                string message = " Name: " + model.Name + " Phone: " + model.Phone + " \n\nEmail: " + model.Email + " \n\nMessages: " + model.Message;
                var smtp = new SmtpClient()
                {
                    Host = ConfigurationManager.AppSettings["HostMail"],
                    Port = int.Parse(ConfigurationManager.AppSettings["PortMail"]),
                    EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSslMail"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = bool.Parse(ConfigurationManager.AppSettings["UseDefaultCredentialsMail"]),
                    Credentials = new NetworkCredential(fromEmail.Address, pass)
                };

                var mess = new MailMessage(fromEmail, toEmail)
                {
                    Subject = title,
                    Body = message
                };
                smtp.Send(mess);
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(ContactFormController), ex, "Error sending contact form.");
                return ex.Message;
            }
        }
    }
}

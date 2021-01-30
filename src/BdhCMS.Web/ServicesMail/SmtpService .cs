using System;
using System.Collections.Generic;
using System.Net.Mail;
using Umbraco.Core.Logging;
using System.Net;
using BdhCMS.Web.ServicesMail;
using BdhCMS.Web.Models;
using BdhCMS.Web.Controllers;
using System.Configuration;
using System.Linq;

namespace BdhCMS.Web.ServicesMail
{
    public class SmtpService : ISmtpService
    {
        private readonly ILogger _logger;

        public SmtpService(ILogger logger)
        {
            _logger = logger;
        }

        public string SendContactToEmail(ContactFormViewModel model,string bodyEmail)
        {
            try
            {
                var fromEmail = new MailAddress(ConfigurationManager.AppSettings["MailAddress"]);
                var pass = ConfigurationManager.AppSettings["EmailPassword"];
                var toEmail = new MailAddress(ConfigurationManager.AppSettings["EmailReceive"]);
                
                string title = string.Format(ConfigurationManager.AppSettings["EmailTitle"], model.Name);

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
                    Body = bodyEmail,
                    IsBodyHtml = true
                };

                var ccMailsString = ConfigurationManager.AppSettings["EmailCC"];
                if (!string.IsNullOrEmpty(ccMailsString))
                {
                    foreach (var mailAddress in ccMailsString.Split(',').Select(x => new MailAddress(x)).ToList())
                    {
                        mess.CC.Add(mailAddress);
                    }
                }
                smtp.Send(mess);
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(ContactFormController), ex, "Error sending contact form.");
                return ex.Message;
            }
        }

        public string SendApplyJobEmail(ApplyJobFormViewModel model,string bodyEmail)
        {
            try
            {
                var fromEmail = new MailAddress(ConfigurationManager.AppSettings["MailAddress"]);
                var pass = ConfigurationManager.AppSettings["EmailPassword"];
                var toEmail = new MailAddress(ConfigurationManager.AppSettings["EmailReceive"]);
                string title = string.Format(ConfigurationManager.AppSettings["EmailApplyJobTitle"], model.Name);

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
                    Body = bodyEmail,
                    IsBodyHtml = true
                };

                foreach (var attachmentFile in model.AttachmentFiles)
                {
                    mess.Attachments.Add(new Attachment(attachmentFile.InputStream,attachmentFile.FileName, attachmentFile.ContentType));
                }

                var ccMailsString = ConfigurationManager.AppSettings["EmailCC"];
                if (!string.IsNullOrEmpty(ccMailsString))
                {
                    foreach (var mailAddress in ccMailsString.Split(',').Select(x => new MailAddress(x)).ToList())
                    {
                        mess.CC.Add(mailAddress);
                    }
                }
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

using BdhCMS.Web.Models;
using BdhCMS.Web.ServicesMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BdhCMS.Web.Helpers;
using Newtonsoft.Json;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;

namespace BdhCMS.Web.Controllers
{
    public class ContactFormController : SurfaceController
    {
        private readonly ISmtpService _smtpService;
        public ContactFormController(ISmtpService smtpService)
        {
            _smtpService = smtpService;
        }

        /// <summary>
        /// Render partial view form sign up an event on page Event detail view
        /// </summary>
        /// <returns></returns>
        public ActionResult RenderSectionContactForm(int currentPageId = 0)
        {
            var prevailEvent = (currentPageId > 0 ) ? Services.ContentService.GetById(currentPageId) : Services.ContentService.GetById(CurrentPage.Id);
            var endDateTime = prevailEvent.GetValue<DateTime?>("endDateTime");
            var model = new ContactFormViewModel {ContactPageId = CurrentPage.Id};

            return PartialView("~/Views/Partials/ContactForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult Submit(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            // Work with form data here
            var result = _smtpService.SendContactToEmail(model, GetEmailBody(model));
            if (!string.IsNullOrEmpty(result))
            {
                ModelState.AddModelError("Summary", result);
                return CurrentUmbracoPage();
            }
            return RedirectToCurrentUmbracoPage();
        }
        private async Task<bool> IsCaptchaValid(string response)
        {
            try
            {
                var secret = "6LdjqckaAAAAAGY9_LlhjLlvnsvgaT9xla3iaMAu";
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        {"secret", secret},
                        {"response", response},
                        {"remoteip", Request.UserHostAddress}
                    };

                    var content = new FormUrlEncodedContent(values);
                    var verify = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                    var captchaResponseJson = await verify.Content.ReadAsStringAsync();
                    var captchaResult = JsonConvert.DeserializeObject<CaptchaResponseViewModel>(captchaResponseJson);
                    return captchaResult.Success
                           && captchaResult.Action == "Submit"
                           && captchaResult.Score > 0.5;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private string GetEmailBody(ContactFormViewModel model)
        {
            return RazorViewToStringRenderer.RenderViewToString(ControllerContext, "~/Views/Partials/EmailTemplates/Contact.cshtml", model, true);
        }
    }
}
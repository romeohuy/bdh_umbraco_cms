using BdhCMS.Web.Models;
using BdhCMS.Web.ServicesMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BdhCMS.Web.Helpers;
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
        public ActionResult RenderSectionContactForm()
        {
            var prevailEvent = Services.ContentService.GetById(CurrentPage.Id);
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

        private string GetEmailBody(ContactFormViewModel model)
        {
            return RazorViewToStringRenderer.RenderViewToString(ControllerContext, "~/Views/Partials/EmailTemplates/Contact.cshtml", model, true);
        }
    }
}
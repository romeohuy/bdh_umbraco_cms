using BdhCMS.Web.Models;
using BdhCMS.Web.ServicesMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;

namespace BdhCMS.Web.Controllers
{
    public class ApplyJobFormController : SurfaceController
    {
        private readonly ISmtpService _smtpService;

        public ApplyJobFormController(ISmtpService smtpService)
        {
            _smtpService = smtpService;
        }

        /// <summary>
        /// Render partial view form sign up an event on page Event detail view
        /// </summary>
        /// <returns></returns>
        public ActionResult RenderSectionApplyJobForm()
        {
            var prevailEvent = Services.ContentService.GetById(CurrentPage.Id);
            var endDateTime = prevailEvent.GetValue<DateTime?>("endDateTime");
            var model = new ApplyJobFormViewModel {CareerDetailPageId = CurrentPage.Id};

            return PartialView("~/Views/Partials/ApplyForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult Submit(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            // Work with form data here
            return RedirectToCurrentUmbracoPage();
        }
    }
}
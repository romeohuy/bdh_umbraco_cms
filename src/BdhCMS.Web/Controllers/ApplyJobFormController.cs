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
        public ActionResult RenderSectionApplyJobForm(int careerDetailId)
        {
            var model = new ApplyJobFormViewModel { CareerDetailPageId = careerDetailId };

            return PartialView("~/Views/Partials/ApplyForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult Submit(ApplyJobFormViewModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            if (model.AttachmentFiles.Any() == false)
            {
                ModelState.AddModelError("Summary", "You need upload CV for apply job.");
                return CurrentUmbracoPage();
            }

            model.CareerDetail = Umbraco.Content(model.CareerDetailPageId) as CareerDetail;

            var result = _smtpService.SendApplyJobEmail(model,GetEmailBody(model));
            if (!string.IsNullOrEmpty(result))
            {
                ModelState.AddModelError("Summary", result);
                return CurrentUmbracoPage();
            }
            // Work with form data here
            return RedirectToCurrentUmbracoPage();
        }

        private string GetEmailBody(ApplyJobFormViewModel model)
        {
            return RazorViewToStringRenderer.RenderViewToString(ControllerContext, "~/Views/Partials/EmailTemplates/ApplyJob.cshtml", model, true);
        }
    }
}
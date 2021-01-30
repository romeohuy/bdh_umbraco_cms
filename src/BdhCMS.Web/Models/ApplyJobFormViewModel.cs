using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedModels;

namespace BdhCMS.Web.Models
{
    public class ApplyJobFormViewModel
    {
        public ApplyJobFormViewModel()
        {
            AttachmentFiles = new List<HttpPostedFileBase>();
        }
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        public string Message { get; set; }

        public List<HttpPostedFileBase> AttachmentFiles { get; set; }
        public int CareerDetailPageId { get; set; }

        public CareerDetail CareerDetail { get; set; }
    }
}
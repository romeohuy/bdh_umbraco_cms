using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BdhCMS.Web.Models
{
    public class ApplyJobFormViewModel
    {
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        public string Message { get; set; }

        public int CareerDetailPageId { get; set; }
    }
}
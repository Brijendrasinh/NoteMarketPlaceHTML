using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class ContactPageLogedUSer
    {
        [Required]
        [Display(Name = "Full Name *")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email Id *")]
        public string EmailId { get; set; }

        [Required]
        [Display(Name = "Subject *")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Comments / Questions")]
        public string Comment { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class SystemConfigurationViewModel
    {
        [Required]
        public string SupportEmail { get; set; }
        [Required]
        public string SupportContactNumber { get; set; }
        [Required]
        public string EmailAddresses { get; set; }
        public string DefaultProfilePicture { get; set; }
        public string DefaultNotePicture { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Linkdin { get; set; }
        
    }
}
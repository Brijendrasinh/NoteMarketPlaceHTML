using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class SystemConfigurationViewModel
    {
        public string SupportEmail { get; set; }
        public string SupportContactNumber { get; set; }
        public string DefaultProfilePicture { get; set; }
        public string DefaultNotePicture { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Linkdin { get; set; }
        public string EmailAddresses { get; set; }
    }
}
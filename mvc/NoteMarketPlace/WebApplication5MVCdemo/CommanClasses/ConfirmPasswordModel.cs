using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class ConfirmPasswordModel
    {
        [Required]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }
       
        [Required]
        [RegularExpression("(?=[A-Za-z0-9@#$%^&+!=]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+!=])(?=.{6,24}).*$", ErrorMessage = "Must Contain 1 Capital,1 Small,1 Digit,1 Special")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        
        [Required]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
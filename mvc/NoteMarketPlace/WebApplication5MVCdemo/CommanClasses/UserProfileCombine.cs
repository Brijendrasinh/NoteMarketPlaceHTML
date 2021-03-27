using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class UserProfileCombine
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserProfile UserProfile { get; set; }
        public IEnumerable<Gender> Genders { get; set; }
        public IEnumerable<Country> Countries { get; set; }
    }
}
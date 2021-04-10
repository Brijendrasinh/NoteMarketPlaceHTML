using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class AdminProfileViewModel
    {
        
        public User user { get; set; }
        public UserProfile userProfile { get; set; }
        public IEnumerable<Country> countries { get; set; }
    }
}
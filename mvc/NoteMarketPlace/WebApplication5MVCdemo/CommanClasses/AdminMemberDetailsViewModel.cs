using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class AdminMemberDetailsViewModel
    {
        public User user { get; set; }
        public UserProfile userProfile { get; set; }
        public int SellerID { get; set; }
       public IEnumerable<NewGetMembersDetails_Result> getMembersDetails_Results { get; set; }
    }
}
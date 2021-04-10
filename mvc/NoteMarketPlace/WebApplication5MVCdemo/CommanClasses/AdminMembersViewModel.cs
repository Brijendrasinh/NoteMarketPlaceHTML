using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class AdminMembersViewModel
    {
        public IEnumerable<NewGetMembersData_Result> getMembersData_Results { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class ManageCategoryViewModel
    {
        public IEnumerable<GetCategoryData_Result> getCategoryData_Results { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class ManageTypeViewModel
    {
        public IEnumerable<GetNoteTypeData_Result> getNoteTypeData_Results { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
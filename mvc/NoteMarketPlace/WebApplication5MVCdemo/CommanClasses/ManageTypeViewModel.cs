using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class ManageTypeViewModel
    {
        public IEnumerable<GetNoteTypeData_Result> getNoteTypeData_Results { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
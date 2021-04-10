﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class ManageCountryViewModel
    {
        public IEnumerable<GetCountryData_Result> getCountryData_Results { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }
}
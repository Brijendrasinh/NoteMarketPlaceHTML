using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace WebApplication5MVCdemo.CommanClasses
{
    public class ManageAdministratorViewModel
    {
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
        public DateTime DateAdded { get; set; }
        public IEnumerable<Country> countries { get; set; }
    }
}
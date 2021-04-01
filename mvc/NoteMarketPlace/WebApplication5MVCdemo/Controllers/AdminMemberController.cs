using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5MVCdemo.Controllers
{
    public class AdminMemberController : Controller
    {
        // GET: AdminMember
        public ActionResult MemberDetail()
        {
            return View();
        }
    }
}
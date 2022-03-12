using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private WebProjectDbEntities db = new WebProjectDbEntities();

        [AllowAnonymous]
        public ActionResult Index()
        {
            //This controller is only for helper methods so there is no index and no authentication. Simply redirect to home page.
            return RedirectToAction("Index", "Home");
        }

       


    }
}
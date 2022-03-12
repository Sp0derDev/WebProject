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

        //This method will return the unpaid cart of the current logged-in user. If the customer or the cart does not exist, both will be created and returned.
        private Cart GetUsersCart()
        {
            //Get the logged in user email
            string loggedInEmail = User.Identity.Name;

            //Does the customer exist in the customers table?
            if (db.Customers.Count(c => c.EmailAddress.Equals(loggedInEmail)) == 0)
            {
                // ^^ If the count is zero this means there is no customer so we add the customer

                //Adding the new customer
                db.Customers.Add(new Customer { EmailAddress = loggedInEmail, Name = loggedInEmail.Split('@')[0] });
                db.SaveChanges();
            }


            Cart cart = null;

            try
            {
                //CHeck if the customer has an unpaid cart.
                cart = db.Carts.First(c => c.CustomerEmail.Equals(loggedInEmail) && c.Status.Equals("Unpaid"));
            }
            catch (Exception)
            {// If not, create a new one.
                cart = new Cart { CustomerEmail = loggedInEmail, Status = "Unpaid" };
                db.Carts.Add(cart);
                db.SaveChanges();
            }
            return cart;
        }




    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            //This controller is only for helper methods so there is no index and no authentication. Simply redirect to home page
            return RedirectToAction("Index", "Home");
        }

        // This method will return the unpaid cart of the current logged-in user. If the customer or the cart does not exist, both will be created and returned
        private Cart GetUsersCart()
        {

            string loggedInEmail = User.Identity.Name; // Get the logged in user email

            // Does the customer exist in the customers table?
            if (db.Customers.Count(c => c.EmailAddress.Equals(loggedInEmail)) == 0) // If the count is zero this means there is no customer so we add the customer
            {

                // Adding the new customer
                db.Customers.Add(new Customer { EmailAddress = loggedInEmail, Name = loggedInEmail.Split('@')[0] });
                db.SaveChanges();
            }


            Cart cart = null;

            try
            {
                // Check if the customer has an unpaid cart
                cart = db.Carts.First(c => c.CustomerEmail.Equals(loggedInEmail) && c.Status.Equals("Unpaid"));
            }
            catch (Exception)
            {   // If not, create a new one
                cart = new Cart { CustomerEmail = loggedInEmail, Status = "Unpaid" };
                db.Carts.Add(cart);
                db.SaveChanges();
            }
            return cart;
        }

        // This method will add the given product to the cart of the logged in user.
        public ActionResult AddToCart(int? id)
        {
            if (id == null) // Is the id of the product provided as a parameter?
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // Return BadRequest error if id is not provided (null)
            }
            Product product = db.Products.Find(id); // Is there a product with the given id?

            if (product == null) { return HttpNotFound(); } // If the product does not exist (null) return NotFound error


            Cart cart = GetUsersCart();// Get the unpaid Cart of the logged in customer


            try
            {
                // Check if the item is already in the cart
                CartItem cartitem = db.CartItems.First(c => (c.CartId == cart.Id && c.ProductId == product.Id));
                cartitem.Quantity++; // Increment quantity if item is in cart
            }
            catch (Exception)
            {
                // If item is not in cart, add it.
                CartItem cartitem = new CartItem { ProductId = product.Id, CartId = cart.Id, Quantity = 1 };
                db.CartItems.Add(cartitem);
            }
            db.SaveChanges();

            return RedirectToAction("Details", "Carts", new { id = cart.Id });  // Finally, take the user to their cart.

        }




    }
}
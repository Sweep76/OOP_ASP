using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace _2103FinalProject.Controllers
{
    public class UserController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        List<Product> products = new List<Product>();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult OrderScreen()
        {
            UserOrderScreen();
            return View(products);
        }

        public ActionResult PayScreen()
        {
            return View();
        }

        public void connectionString()
        {
            con.ConnectionString = "data source=LAPTOP-NSOLU8N9\\SQLEXPRESS;initial catalog=Computerbranch;integrated security=True;";
        }

        public ActionResult OrderCode(FormCollection fc)
        {
            ComputerbranchEntities tb = new ComputerbranchEntities();
            String prodDesc = fc["ProductDesc"];
            String productID = fc["ProductID"];
            String productPrice = fc["ProductPrice"];
            String quantity = fc["Quantity"];
            //String status = "Pending";

            Order o = new Order();
            o.ProductDesc = prodDesc;
            o.ProductID = Convert.ToInt32(productID);
            o.Quantity = Convert.ToInt32(quantity);
            o.ProductPrice = Convert.ToInt32(productPrice);
            //o.Status = status;

            tb.Orders.Add(o);
            tb.SaveChanges();

            return View("Payscreen");
        }
        public ActionResult RegisterCode(FormCollection fc)
        {

            ComputerbranchEntities tb = new ComputerbranchEntities();

            String firstName = fc["FirstName"];
            String lastName = fc["LastName"];
            String emailAdd = fc["EmailAdd"];
            String password = fc["Password"];
            String role = "Customer";

            User u = new User();
            u.FirstName = firstName;
            u.LastName = lastName;
            u.Email = emailAdd;
            u.Password = password;
            u.Role = role;

            tb.Users.Add(u);
            tb.SaveChanges();

            return View("OrderScreen");
        }

        public ActionResult EntryCode(User User)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM Users WHERE Email='" + User.Email + "' and Password='" + User.Password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return View();
            }
            else
            {
                con.Close();
                return RedirectToAction("OrderScreen");
            }
        }


        [HttpPost]
        private void UserOrderScreen()
        {
            connectionString();

            if (products.Count > 0)
            {
                products.Clear();
            }

            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT ProductID, ProductName, ProductPrice FROM Products;";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    products.Add(new Product()
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        ProductPrice = Convert.ToInt32(dr["ProductPrice"]),
                    });
                }
                con.Close();
            }
            catch (Exception e)
            {

            }
        }
        public ActionResult AccountEditCode(FormCollection fc)
        {
            ComputerbranchEntities tb = new ComputerbranchEntities();
            int CurrentUserID = 1;

            var user = (from p in tb.Users
                        where p.UserID == CurrentUserID
                        select p).FirstOrDefault();

            user.FirstName = "Christian";
            
            tb.SaveChanges();

            return View();
        }

        public ActionResult AccountDeleteCode(FormCollection fc)
        {

            ComputerbranchEntities tb = new ComputerbranchEntities();
            int CurrentUserID = 2;

            var user = (from p in tb.Users
                        where p.UserID == CurrentUserID
                        select p).FirstOrDefault();
            if (user != null)
            {
                tb.Users.Remove(user);
                tb.SaveChanges();
                ViewData["DeleteStatus"] = "User has successfully been deleted!";
            }
            else
            {
                ViewData["DeleteStatus"] = "User has not been deleted!";
            }

            return View();
        }

        public ActionResult AccountRetrieveCode(FormCollection fc)
        {

            ComputerbranchEntities tb = new ComputerbranchEntities();

            var user = (from p in tb.Users
                        select p).ToList();
            if (user != null)
            {
                ViewData["UserList"] = "Success";
                ViewData["RetrieveStatus"] = "Success";
            }
            else
            {
                ViewData["RetrieveStatus"] = "Failed";
            }

            return View();
        }

        public ActionResult ProductRetrieveCode(FormCollection fc)
        {

            ComputerbranchEntities tb = new ComputerbranchEntities();

            var product = (from p in tb.Products
                        select p).ToList();
            if (product != null)
            {
                ViewData["ProductList"] = "Success";
                ViewData["RetrieveStatus"] = "Success";
            }
            else
            {
                ViewData["RetrieveStatus"] = "Failed";
            }

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using _2103FinalProject;
using System.Drawing;
using System.Data.SqlClient;

namespace _2103FinalProject.Controllers
{
    public class ManagerController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        // GET: Manager
        public ActionResult OrderView()
        {
            using (ComputerbranchEntities db = new ComputerbranchEntities())
            {
                return View(db.Orders.ToList());
            }
        }

        // GET: Manager/Details/5
        public ActionResult OrderDetails(int id)
        {
            using (ComputerbranchEntities tb = new ComputerbranchEntities())
            {
                return View(tb.Orders.Where(x => x.OrderID == id).FirstOrDefault());
            }
        }


        // GET: Manager/Edit/5
        public ActionResult Edit(int id)
        {
            using (ComputerbranchEntities tb = new ComputerbranchEntities())
            {
                return View(tb.Orders.Where(x => x.OrderID == id).FirstOrDefault());
            }
        }

        // POST: Manager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Order Order)
        {
            try
            {
                // TODO: Add update logic here
                using (ComputerbranchEntities tb = new ComputerbranchEntities())
                {
                    tb.Entry(Order).State = EntityState.Modified;
                    tb.SaveChanges();
                }
                return RedirectToAction("OrderView");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public void connectionString()
        {
            con.ConnectionString = "data source=LAPTOP-NSOLU8N9\\SQLEXPRESS;initial catalog=Computerbranch;integrated security=True;";
        }
        public ActionResult ManagerEntryCode(User User)
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
                return RedirectToAction("OrderView");
            }
        }
    }
}

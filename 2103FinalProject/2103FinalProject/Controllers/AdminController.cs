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
    public class AdminController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            using (ComputerbranchEntities db = new ComputerbranchEntities())
            {
                return View(db.Users.ToList());
            }
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            using (ComputerbranchEntities tb = new ComputerbranchEntities())
            {
                return View(tb.Users.Where(x=>x.UserID == id).FirstOrDefault());
            }   
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(User User)
        {
            try
            {
                // TODO: Add insert logic here
                using(ComputerbranchEntities tb = new ComputerbranchEntities())
                {
                    tb.Users.Add(User);
                    tb.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            using (ComputerbranchEntities tb = new ComputerbranchEntities())
            {
                return View(tb.Users.Where(x => x.UserID == id).FirstOrDefault());
            }
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User User)
        {
            try
            {
                // TODO: Add update logic here
                using (ComputerbranchEntities tb = new ComputerbranchEntities())
                {
                    tb.Entry(User).State = EntityState.Modified;
                    tb.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //GET: Admin/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (ComputerbranchEntities tb = new ComputerbranchEntities())
            {
                return View(tb.Users.Where(x => x.UserID == id).FirstOrDefault());
            }
        }

        //POST: Admin/Delete
        public ActionResult Delete(int id, User User)
        {
            try
            {
                // TODO: Add update logic here
                using (ComputerbranchEntities tb = new ComputerbranchEntities())
                {
                    User = tb.Users.Where(x => x.UserID == id).FirstOrDefault();
                    tb.Users.Remove(User);
                    tb.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void connectionString()
        { //data source=LAPTOP-NSOLU8N9\SQLEXPRESS;initial catalog=tastebuds_database_schema;integrated security=True;
            con.ConnectionString = "data source=LAPTOP-NSOLU8N9\\SQLEXPRESS;initial catalog=Computerbranch;integrated security=True;";
        }
        [HttpPost]
        public ActionResult AdminEntryCode(User User)
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
                return RedirectToAction("Index");
            }
        }
    }
}

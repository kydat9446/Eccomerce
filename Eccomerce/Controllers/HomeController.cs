using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Eccomerce.Models;
using Eccomerce.Areas.Admin.Data;
using Eccomerce.Areas.Admin.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
namespace Eccomerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly DPContext _context;

        public HomeController(DPContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.TypeProduct = _context.typeProduct;
            ViewBag.ListProduct = _context.product;
            //T-Shirts
            var tShirts = (from products in _context.product
                           where products.Catid == 1
                           orderby products.Id descending
                           select products
                           ).ToList();
            ViewBag.TShirt = tShirts;
            //Pant
            var Pant = (from products in _context.product
                           where products.Catid == 2
                           orderby products.Id descending
                           select products
                           ).ToList();
            ViewBag.Pant = Pant;
            //Shoes
            var Shoes = (from products in _context.product
                           where products.Catid == 3
                           orderby products.Id descending
                           select products
                           ).ToList();
            ViewBag.Shoes = Shoes;
            //Hoodie
            var Hoodie = (from products in _context.product
                           where products.Catid == 4
                           orderby products.Id descending
                           select products
                           ).ToList();
            ViewBag.Hoodie = Hoodie;

            return View();
        }

        public IActionResult Men()
        {
            return View();
        }

        public IActionResult Single()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Account _account)
        {
            if (ModelState.IsValid)
            {
                var check = _context.account.FirstOrDefault(s => s.Email == _account.Email);
                if (check == null)
                {
                    
                    _account.Catid =1;

                    _account.Password = GetMD5(_account.Password);
                    _context.account.Add(_account);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


               var f_password = GetMD5(password);
                var data = _context.account.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                  
                        HttpContext.Session.SetString("Name", data.FirstOrDefault().Name);
                    HttpContext.Session.SetInt32("idUser", data.FirstOrDefault().Id);
                    //Login admin
                    if (data[0].Catid == 2)
                    {
                        var url = Url.RouteUrl("default", new { controller = "Accounts", action = "Index", area = "Admin" });
                        return Redirect(url);
                    }
                    //Login user
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return View();
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public JsonResult LoginFB(string Name)
        {  
            HttpContext.Session.SetString("Name", Name);
            return Json("");
        }


        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}

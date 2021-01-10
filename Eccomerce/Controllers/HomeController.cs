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
using Newtonsoft.Json;

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
            ViewBag.Products = _context.product;
            return View();
        }

        public IActionResult Single(int? id)
        {
            var product = _context.product.Where(item => item.Id == id).ToList();
            ViewBag.Product = product;

            var sameProducts = (from p in product
                                join c in _context.product on p.Catid equals c.Catid
                                orderby c.Id descending
                                select c
                                ).Take(4).ToList();
            ViewBag.SameProducts = sameProducts;
            var productsNew = (from p in _context.product
                               orderby p.Id descending
                               select p
                             ).Take(8).ToList();
            ViewBag.NewProducts = productsNew;
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

        public IActionResult ListCartEmty()
        {
            return View();
        }

        //GET ALL PRODUCT
        public List<Product> getAllProduct()
        {
            return _context.product.ToList();
        }

        //GET DETAIL PRODUCT
        public Product getDetailProduct(int id)
        {
            var product = _context.product.Find(id);
            return product;
        }

        //ADD CART
        public IActionResult addCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart == null)
            {
                var product = getDetailProduct(id);
                List<Cart> listCart = new List<Cart>()
               {
                   new Cart
                   {
                       Product = product,
                       Quantity = 1
                   }
               };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));

            }
            else
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                bool check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Product.Id == id)
                    {
                        dataCart[i].Quantity++;
                        check = false;
                    }
                }
                if (check)
                {
                    dataCart.Add(new Cart
                    {
                        Product = getDetailProduct(id),
                        Quantity = 1
                    });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                // var cart2 = HttpContext.Session.GetString("cart");//get key cart
                //  return Json(cart2);
            }

            return RedirectToAction(nameof(Index));

        }

        //Gọi tới trang cart
        public IActionResult ListCart()
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                    return View();
                }
            }
            return View("ListCartEmty");
        }

        //Update Cart
        [HttpPost]
        public IActionResult updateCart(int id, int quantity)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (quantity > 0)
                {
                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].Product.Id == id)
                        {
                            dataCart[i].Quantity = quantity;
                        }
                    }


                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                }
                var cart2 = HttpContext.Session.GetString("cart");
                return Ok(quantity);
            }
            return BadRequest();

        }

        //Xóa Cart
        public IActionResult deleteCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);

                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Product.Id == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                return RedirectToAction(nameof(ListCart));
            }
            return RedirectToAction(nameof(Index));
        }


    }
}

using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class WebsiteController : Controller
    {
        public AppDbContext _context;
        public EmailSender _emailsender;
        public WebsiteController(AppDbContext context, EmailSender emailsender)
        {
            _context = context;
            _emailsender = emailsender;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(HttpContext.Session.GetString("login")!=null)
            {
                TempData["signup"] = "success";
            }

            base.OnActionExecuting(context);
        }

        public IActionResult Index()
        {
            var data = _context.slider.ToList();
            var data1 = _context.collection.ToList();
            var data2 = _context.topsales.ToList();
            var data3 = _context.newarrival.ToList();
            var data4 = _context.category.Where(x=>x.visiblestatus == true).ToList();

            var alldata = new HomePage
            {
                slider = data,
                collection = data1,
                topsales = data2,
                newarrival = data3,
                category = data4,
            };


            return View(alldata);
        }
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminLogin(string email, string password)
        {
            var data = _context.adminlogin.FirstOrDefault(x => x.email == email && x.password == password);
            if (data != null)
            {
                HttpContext.Session.SetString("admin", email);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                TempData["alert"] = "emailpass_incorrect";
                return RedirectToAction("AdminLogin");
            }
        }
        //SignUp Function
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(signup s, string password, string confirmpass )
        {
            HttpContext.Session.GetString("login");
            if(password == confirmpass)
            {
                _context.signup.Add(s);
                _context.SaveChanges();
                TempData["signup"] = "success";
                TempData["username"] = s.fullname;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["signup"] = "Confirm Password Not Matched";
                return RedirectToAction("Signup");
            }
        }
        //Login Function 
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var data = _context.signup.FirstOrDefault(x => x.email == email && x.password == password);
            if (data != null)
            {
                HttpContext.Session.SetString("login", email);
                //HttpContext.Session.SetString("userid", data.id);
                TempData["login"] = "loginsuccess";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["login"] = "Email or Password Is Incorrect";
                return RedirectToAction("Login");
            }
        }

        //CHANGE PASSWORD
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(string oldpass, string newpass, string confirmpass)
        {
            string email = HttpContext.Session.GetString("login");
            var data = _context.signup.FirstOrDefault(x => x.email == email);
            if (data.password == oldpass)
            {
                if (newpass == confirmpass)
                {
                    data.password = newpass;
                    _context.signup.Update(data);
                    _context.SaveChanges();
                    TempData["signup"] = "success";
                    return RedirectToAction("Profile");
                }
                else
                {
                    TempData["msg"] = "Confirm password not mached";
                    return RedirectToAction("ChangePassword");
                }
            }
            else
            {
                TempData["msg"] = "Old Password not mached";
                return RedirectToAction("ChangePassword");
            }
        }
        //LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["signup"] = null;
            return RedirectToAction("Index");
        }
        //FORGOT PASSWORD
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var data = _context.signup.FirstOrDefault(x => x.email == email);
            if (data != null)
            {
                Random r = new Random();
                int otp = r.Next(1000, 9999);

                HttpContext.Session.SetString("otp", otp.ToString());
                HttpContext.Session.SetString("email", email);

                string sendto = email;
                string subject = "OTP For Reset Password";
                string mail = "Dear User, Your OTP For Reset Password Is - " + otp;
                await _emailsender.SendEmailAsync(sendto, subject, mail);

                return RedirectToAction("ResetPassword");
            }
            else
            {
                TempData["msg"] = "This Email Is Not Registered";
                return RedirectToAction("ForgotPassword");
            }
        }
        //RESET PASSWORD
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(string myotp, string newpass, string confirmpass)
        {
            string mainotp = HttpContext.Session.GetString("otp");
            if(myotp == mainotp)
            {
                if(newpass == confirmpass)
                {
                    string email = HttpContext.Session.GetString("email");
                    var data = _context.signup.FirstOrDefault(x => x.email == email);
                    data.password = newpass;
                    _context.signup.Update(data);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["error"] = "Your Confirm Password Is Not Mached";
                    return RedirectToAction("RestPassword");
                }
            }
            else
            {
                TempData["error"] = "Your Otp Is Not Mached";
                TempData["error"] = "Your Otp Is Not Mached";
                return RedirectToAction("ResetPassword");
            }
        }
        //PROFILE VIEW
        public IActionResult Profile()
        {
            string email = HttpContext.Session.GetString("login");
            var data = _context.signup.FirstOrDefault(x => x.email == email);
            return View(data);
        }
        //update user data 
        public IActionResult EditUser(int id)
        {
            var data = _context.signup.Find(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult EditUser(int id, signup s)
        {
            var data = _context.signup.Find(s.id);

            data.fullname = s.fullname;
            data.email = s.email;
            data.mobile = s.mobile;

            _context.signup.Update(data);
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }
        //shoes collection
        public IActionResult ShoesCollection()
        {
            var data = _context.shoescollection.ToList();
            return View(data);
        }
        //Mens collection
        public IActionResult MensCollection()
        {
            var data = _context.menscollection.ToList();
            return View(data);
        }
        //TOPSALES PRODUCTS DETAILS
        public IActionResult ProductDetails(int id)
        {
            var data = _context.product.Find(id);
            return View(data);
        }
        //PRODUCT DETAILS
         public IActionResult Products(int id)
        {
            var data = _context.product.Where(x => x.cid == id).ToList();
            return View(data);
        }


        //ORDER NOW
        public IActionResult BuyNow(int id)
        {
            if (HttpContext.Session.GetString("login") == null)
            {
                return RedirectToAction("Login");
            }
            var data = _context.product.Find(id);
            return View(data);
        }
        // pay now
        [HttpPost]
        public IActionResult OrderNow(IFormCollection form)
        {
            order order = new order();
            order.name = form["name"];
            order.email = form["email"];
            order.phone = form["phone"];
            order.pincode = form["pincode"];
            order.address1 = form["address1"];
            order.address2 = form["address2"];
            order.city = form["city"];
            order.country = form["country"];
            order.state = form["state"];
            order.productid = form["id"];

            var data = _context.product.Find(int.Parse(form["id"].ToString()));
            // var data = _context.product.find(form["id"]);

            order.productname = data.name;
            order.productprice = data.price1;
            order.paymentmode = form["paymentmode"];

            order.paymentstatus = "pending";
            order.transactionid = "0";

            _context.order.Add(order);
            _context.SaveChanges();

            if (form["paymentmode"] == "cod")
            {
                return RedirectToAction("OrderPlaced");
            }
            else
            {
                TempData["name"] = order.name;
                TempData["email"] = order.email;
                TempData["mobile"] = order.phone;
                TempData["amount"] = data.price1;
                TempData["orderid"] = order.id;

                return RedirectToAction("PayNow");
            }
        }
        //ORDER PLACED
        public IActionResult OrderPlaced()
        {
            return View();
        }
        //ONLINE PAYMENT
        //paying
        public IActionResult PayNow()
        {
            return View();
        }
        public IActionResult PaymentSuccess()
        {
            string txnid = Request.Query["txnid"];
            string orderid = Request.Query["orderid"];

            var data = _context.order.Find(int.Parse(orderid));

            data.transactionid = txnid;
            data.paymentstatus = "success";

            _context.order.Update(data);
            _context.SaveChanges();

            TempData["alert"] = "payment_success";
            return RedirectToAction("OrderPlaced");
        }
        public IActionResult PaymentFailed()
        {
            return View();
        }


    }
}

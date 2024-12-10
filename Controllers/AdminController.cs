using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class AdminController : Controller
    {
        public AppDbContext _context;
        public IWebHostEnvironment _environment;
        public AdminController(AppDbContext context, IWebHostEnvironment environment)
        { 
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Index", "Website");
            }
            return View();
        }
        //Admin logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Website");
        }
        //Add Slider
        public IActionResult Slider()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Index", "Website");
            }

            var data = _context.slider.ToList();
            return View(data);
        }
        //add slider image
        [HttpPost]
        public async Task<IActionResult> AddSlider(IFormFile image, string heading, string sliderlink)
        {
            string folderpath = Path.Combine(_environment.WebRootPath, "slider");
            string filename = image.FileName;
            string filepath = Path.Combine(folderpath, filename);
            var stream = new FileStream(filepath, FileMode.Create);
            await image.CopyToAsync(stream);

            slider s = new slider();
            s.image = filename;
            s.heading = heading;
            s.sliderlink = sliderlink;

            _context.slider.Add(s);
            _context.SaveChanges();

            return RedirectToAction("Slider");
        }
        //sliderimage delete
        public IActionResult DeleteSlider(int id)
        {
            var data = _context.slider.Find(id);
            string filename = data.image;
            if (filename != null)
            {
                string folderpath = Path.Combine(_environment.WebRootPath, "slider");
                string filepath = Path.Combine(folderpath, filename);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }

            _context.slider.Remove(data);
            _context.SaveChanges();

            return RedirectToAction("Slider", "Admin");
        }

        // add collection image
        public IActionResult Collection()
        { 
            var data = _context.collection.ToList();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddCollection(IFormFile image, string collectionname, string collectionlink)
        {
            string folderpath = Path.Combine(_environment.WebRootPath, "collection");
            string filename = image.FileName;
            string filepath = Path.Combine(folderpath, filename);
            var stream = new FileStream(filepath, FileMode.Create);
            await image.CopyToAsync(stream);

            collection c = new collection();
            c.image = filename;
            c.collectionname = collectionname;
            c.collectionlink = collectionlink;

            _context.collection.Add(c);
            _context.SaveChanges();

            return RedirectToAction("Collection");
        }
        //delete collection
        public IActionResult DeleteCollection(int id)
        {
            var data = _context.collection.Find(id);
            string filename = data.image;
            if (filename != null)
            {
                string folderpath = Path.Combine(_environment.WebRootPath, "collection");
                string filepath = Path.Combine(folderpath, filename);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }

            _context.collection.Remove(data);
            _context.SaveChanges();

            return RedirectToAction("Collection", "Admin");
        }

        //Add top sales
        public IActionResult Topsales()
        {
            var data = _context.topsales.ToList();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddTopItem(IFormFile image, string name, string price1, string price2, string discount)
        {
            string folderpath = Path.Combine(_environment.WebRootPath, "topsales");
            string filename = image.FileName;
            string filepath = Path.Combine(folderpath, filename);
            var stream = new FileStream(filepath, FileMode.Create);
            await image.CopyToAsync(stream);

            topsales t = new topsales();
            t.image = filename;
            t.name = name;
            t.price1 = price1;
            t.price2 = price2;
            t.discount = discount;

            _context.topsales.Add(t);
            _context.SaveChanges();

            return RedirectToAction("Topsales");
        }
        // delete top items
        public IActionResult DeleteTopItem(int id)
        {
            var data = _context.topsales.Find(id);
            string filename = data.image;
            if (filename != null)
            {
                string folderpath = Path.Combine(_environment.WebRootPath, "topsales");
                string filepath = Path.Combine(folderpath, filename);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }

            _context.topsales.Remove(data);
            _context.SaveChanges();

            return RedirectToAction("Topsales", "Admin");
        }
        // Add new arrival
        public IActionResult NewArrival()
        {
            var data = _context.newarrival.ToList();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> NewArrival(IFormFile image, string name, string price1, string price2, string discount)
        {
            string folderpath = Path.Combine(_environment.WebRootPath, "newarrival");
            string filename = image.FileName;
            string filepath = Path.Combine(folderpath, filename);
            var stream = new FileStream(filepath, FileMode.Create);
            await image.CopyToAsync(stream);

            newarrival n = new newarrival();
            n.image = filename;
            n.name = name;
            n.price1 = price1;
            n.price2 = price2;
            n.discount = discount;

            _context.newarrival.Add(n);
            _context.SaveChanges();

            return RedirectToAction("NewArrival");
        }
        // delete top items
        public IActionResult DeleteArrival(int id)
        {
            var data = _context.newarrival.Find(id);
            string filename = data.image;
            if (filename != null)
            {
                string folderpath = Path.Combine(_environment.WebRootPath, "newarrival");
                string filepath = Path.Combine(folderpath, filename);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }

            _context.newarrival.Remove(data);
            _context.SaveChanges();

            return RedirectToAction("NewArrival", "Admin");
        }
        //manage user
        public IActionResult Users()
        {
            var data = _context.signup.ToList();
            return View(data);
        }
        //delete user
        public IActionResult DeleteUser(int id)
        {
            var data = _context.signup.Find(id);
     
            _context.signup.Remove(data);
            _context.SaveChanges();

            return RedirectToAction("Users", "Admin");
        }
        // SHOES COLLECTION
        public IActionResult ShoesCollection()
        {
            var data = _context.shoescollection.ToList();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> ShoesCollection(IFormFile image, string name, string price1, string price2, string discount)
        {
            string folderpath = Path.Combine(_environment.WebRootPath, "shoescollection");
            string filename = image.FileName;
            string filepath = Path.Combine(folderpath, filename);
            var strem = new FileStream(filepath, FileMode.Create);
            await image.CopyToAsync(strem);

            shoescollection s = new shoescollection();
            s.name = name;
            s.image = filename;
            s.price1 = price1;
            s.price2 = price2;
            s.discount = discount;

            _context.shoescollection.Add(s);
            _context.SaveChanges();
            return RedirectToAction("ShoesCollection");

        }
        //DELETE SHOES 
        public IActionResult DeleteShoes(int id)
        {
            var data = _context.shoescollection.Find(id);
            string filename = data.image;
            if(filename != null)
            {
                string folderpath = Path.Combine(_environment.WebRootPath, "shoescollection");
                string filepath = Path.Combine(folderpath, filename);
                if(System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }

            _context.shoescollection.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("ShoesCollection", "Admin");
        }

        // MENS COLLECTION
        public IActionResult MensCollection()
        {
            var data = _context.menscollection.ToList();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> MensCollection(IFormFile image, string name, string price1, string price2, string discount)
        {
            string folderpath = Path.Combine(_environment.WebRootPath, "menscollection");
            string filename = image.FileName;
            string filepath = Path.Combine(folderpath, filename);
            var stream = new FileStream(filepath, FileMode.Create);
            await image.CopyToAsync(stream);

            menscollection m = new menscollection();
            m.name = name;
            m.image = filename;
            m.price1 = price1;
            m.price2 = price2;
            m.discount = discount;

            _context.menscollection.Add(m);
            _context.SaveChanges();
            return RedirectToAction("MensCollection");
        }
        //DELETE mens 
        public IActionResult DeleteMens(int id)
        {
            var data = _context.menscollection.Find(id);
            string filename = data.image;
            if (filename != null)
            {
                string folderpath = Path.Combine(_environment.WebRootPath, "menscollection");
                string filepath = Path.Combine(folderpath, filename);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }

            _context.menscollection.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("MensCollection", "Admin");
        }

        //CATEGORY SECTION
        public IActionResult Category()
        {
            var data = _context.category.ToList();
            return View(data);
        }

        public IActionResult CategoryStatus(int id)
        {
            var data = _context.category.Find(id);
            if (data.visiblestatus == null || data.visiblestatus == false)
            {
                data.visiblestatus = true;
            }
            else
            {
                data.visiblestatus = false;
            }

            _context.category.Update(data);
            _context.SaveChanges();
            return RedirectToAction("Category");
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategory(string title, string description, IFormFile picture)
        {
            string folderpath = Path.Combine(_environment.WebRootPath,"category");
            string filename = picture.FileName;
            string filepath = Path.Combine(folderpath, filename);
            var stream = new FileStream(filepath, FileMode.Create);
            await picture.CopyToAsync(stream);

            category c = new category();
            c.title = title;
            c.description = description;
            c.picture = filename;

            _context.category.Add(c);
            _context.SaveChanges();
            return RedirectToAction("Category");

        }
        //DELETE CATEGORY
        public IActionResult DeleteCategory(int id)
        {
            var data = _context.category.Find(id);
            string filename = data.picture;
            if (filename != null)
            {
                string folderpath = Path.Combine(_environment.WebRootPath, "category");
                string filepath = Path.Combine(folderpath, filename);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }

            _context.category.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Category", "Admin");
        }

        //PRODUCTS
        public IActionResult Product()
        {
            ViewBag.categories = _context.category.ToList();

            var data = _context.product.ToList();

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> SaveProduct(string name, string price1, string price2, IFormFile image, string cid,string discount)
        {
            string folderpath = Path.Combine(_environment.WebRootPath, "product");
            string filename = image.FileName;
            string filepath = Path.Combine(folderpath, filename);
            var stream = new FileStream(filepath, FileMode.Create);
            await image.CopyToAsync(stream);

            product p = new product();
            p.name = name;
            p.price1 = price1;
            p.price2 = price2;
            p.discount = discount;
            p.image = filename;
            p.cid = int.Parse(cid);

            _context.product.Add(p);
            _context.SaveChanges();

            return RedirectToAction("Product");


        }
        //DELETE PRODUCT
        public IActionResult DeleteProduct(int id)
        {
            var data = _context.product.Find(id);
            string filename = data.image;
            if (filename != null)
            {
                string folderpath = Path.Combine(_environment.WebRootPath, "product");
                string filepath = Path.Combine(folderpath, filename);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }

            _context.product.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Product", "Admin");
        }

        //manage order
        public IActionResult Order(int id)
        {
            var data = _context.order.ToList();
            return View(data);
        }
        // DELETE ORDER
        public IActionResult DeleteOrder(int id)
        {
            var data = _context.order.Find(id);

            _context.order.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Order");
        }
        // INVOICOE
        public IActionResult Invoice(int id)
        {
            var data = _context.order.Find(id);
            return View(data);
        }

    }
}

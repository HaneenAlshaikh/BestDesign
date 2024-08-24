using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using souq.Data;
using souq.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace souq.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            indexMV Result=new indexMV();
            Result.Categories=_context.Categorys.ToList();
            Result.Products=_context.Producs.ToList();
            return View(Result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetProductsByCategory(int id)
        {
            var pro = _context.Producs.Where(x =>x.categoryId == id).ToList();
            return View(pro);
        }
        public IActionResult ProductSearch(String xname)
        {
            var pro = _context.Producs.Where(x => x.subject.Contains(xname)).ToList();
            return View(pro);
        }
        [HttpGet]
        public IActionResult SendReview()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendReview(Review review)
        {
            _context.Reviews.Add(new Review
             {
                Subject=review.Subject,
                Name=review.Name,
                Email=review.Email,
                Message=review.Message

            }) ;
           
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult CurrentProduct(int id)
        {
            var product = _context.Producs.Include(x=>x.category).FirstOrDefault(x=>x.id==id);
            return View(product);
        }
        public IActionResult FilterPrice()
        {
           indexMV Result=new indexMV();
            Result.Products=_context.Producs.OrderByDescending(x => x.price).ToList();
            return View(Result);
        }
        [Authorize]
        public IActionResult AddProductCart(int id)
        {
            var price = _context.Producs.Find(id).price;
            var item=_context.Carts.FirstOrDefault(x=>x.ProductId==id && 
            x.userId==User.Identity.Name);
            if(item!=null)
            {
                item.quantity += 1;
            }
            else
            {
                _context.Carts.Add(new Cart
                {
                    ProductId = id,
                    userId = User.Identity.Name,
                    quantity = 1,
                    price=(int)price
                }) ;
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddOrder()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            Order o = new Order
            {
                Id = order.Id,
                Name = order.Name,
                Email = order.Email,
                Address = order.Address,
                userId = User.Identity.Name,
               
            };

            var cartItem = _context.Carts.Where(x => x.userId == User.Identity.Name).ToList();

            _context.Carts.RemoveRange(cartItem);
            _context.Orders.Add(o);
            _context.SaveChanges();

            return Redirect("~/Carts/Index");

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      
    }
}

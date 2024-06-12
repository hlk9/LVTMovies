using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Context;
using Net.payOS.Types;
using Net.payOS;
using Movies.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Movies.WebApp.Controllers
{
    public class BillController : Controller
    {
        MovieDbContext _context;

        public BillController()
        {
            _context = new MovieDbContext();
        }

        public IActionResult ListBillManager()
        {
            return View();
        }

        public IActionResult CheckOut(int mId)
        {
            if (HttpContext.Session.GetString("id") == null)
            {
                return RedirectToAction("Login", "Asset");
            }

            Guid uid = Guid.Parse(HttpContext.Session.GetString("id"));

            var adf = _context.Rentals.Where(x => x.UserID == uid && x.MovieID == mId && x.RentalDate < DateTime.Now && x.ReturnDate > DateTime.Now).FirstOrDefault();
            if (adf != null)
            {
                ViewBag.isRented = true;
            }

            var mov = _context.Movies.Find(mId);

            return View(mov);
        }
        public async Task<IActionResult> GoToPayment(int mId)
        {

            if (HttpContext.Session.GetString("id") == null)
            {
                return RedirectToAction("Login", "Asset");
            }
            var uid= HttpContext.Session.GetString("id");
            var mov = _context.Movies.Find(mId);
            if (mov == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString("rentMID", mov.Id.ToString());
            HttpContext.Session.SetString("rentPrice", mov.RentalPrice.ToString());


            PayOS payOS = new PayOS("09b8a42b-6105-4cd4-a4ee-8492e42e909c", "15cfbaf8-79a4-48a0-908f-248c30538001", "00b20c6b94e21bf27e6cb0ae2f26515637c93d70b2eeb832e7b51e299cba433d");
            ItemData item = new ItemData(mov.Title, 1, (int)mov.RentalPrice);
            List<ItemData> items = new List<ItemData>();
            items.Add(item);
            int ordCode = new Random().Next(1, int.MaxValue);
            PaymentData paymentData = new PaymentData(ordCode, (int)mov.RentalPrice, "LVT Movies - Thuê phim", items, "https://localhost:7154/Bill/PaymentCanceled", "https://localhost:7154/Bill/PaymentProcessing?rentMID=" + mov.Id+"&price="+mov.RentalPrice+"&uid="+uid);

            CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
            return Redirect(createPayment.checkoutUrl);
        }

        public IActionResult PaymentProcessing(int rentMID, int price, string uid)
        {
            foreach(var item in _context.Rentals.Where(x=>x.UserID == Guid.Parse(uid) && x.MovieID == rentMID).ToList())
            {
                if(item.RentalDate<=DateTime.Now && item.ReturnDate>=DateTime.Now)
                {
                    return RedirectToAction("PaymentSuccess", new { mID = rentMID });
                }
            }
          
            try
            {
                Rental rent = new Rental();
                rent.MovieID = rentMID;
                rent.UserID = Guid.Parse(uid);
                rent.RentalDate = DateTime.Now;
                rent.Status = 1;
                rent.ReturnDate = DateTime.Now.AddDays(7);
                _context.Rentals.Add(rent);
                _context.SaveChanges();

                Payment pay = new Payment();
                pay.PaymentDate = DateTime.Now;
                pay.Status = 1;
                pay.Amount = price;
                pay.UserID = Guid.Parse(uid);
                pay.RentalID = rent.Id;
                _context.Payments.Add(pay);
                _context.SaveChanges();
                int mId = rentMID;
                return RedirectToAction("PaymentSuccess", new { mId });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult PaymentSuccess(int mId)
        {
            var mov = _context.Movies.Find(mId);
            ViewBag.Title = mov.Title;
            return View();
        }
      
        public IActionResult PaymentCanceled(string status)
        {

            return View();

        }

        public IActionResult MyBill()
        {
            Guid uid = Guid.Parse(HttpContext.Session.GetString("id"));

            var listRent  = _context.Rentals.Where(x => x.UserID == uid).ToList();

            var lst = (from rent in listRent
                      join mov in _context.Movies on rent.MovieID equals mov.Id
                      where rent.UserID ==uid
                      select new Movies.DAL.ViewModels.BillMovieViewModel
                      {
                          RentalId = rent.Id,
                          MovieName = mov.Title,
                          UserName = _context.Users.Find(rent.UserID).UserName,
                          RentalDate = rent.RentalDate,
                          ReturnDate = rent.ReturnDate,
                          Status = rent.ReturnDate < DateTime.Now ? false : true,
                          Price =1111
                      }).OrderByDescending(x=>x.ReturnDate).ToList();
            return View(lst);
        }
    }
}

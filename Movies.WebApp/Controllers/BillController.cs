using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Context;
using Net.payOS.Types;
using Net.payOS;

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

        public IActionResult CheckOut()
        {
            return View();
        }
        public async Task<IActionResult> GoToPayment()
        {

            PayOS payOS = new PayOS("09b8a42b-6105-4cd4-a4ee-8492e42e909c", "15cfbaf8-79a4-48a0-908f-248c30538001", "00b20c6b94e21bf27e6cb0ae2f26515637c93d70b2eeb832e7b51e299cba433d");
            ItemData item = new ItemData("Người kim loại", 1, 59000);
            List<ItemData> items = new List<ItemData>();
            items.Add(item);
            int ordCode = new Random().Next(1, int.MaxValue);
            PaymentData paymentData = new PaymentData(ordCode, 59000, "LVT Movies - IronMan", items, "https://localhost:7154/Bill/PaymentCanceled", "https://localhost:7154/Bill/PaymentSuccess");

            CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
            return Redirect(createPayment.checkoutUrl);
        }

        public IActionResult PaymentSuccess()
        {

            return View();

        }
        public IActionResult PaymentCanceled(string status)
        {

            return View();

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Context;
using Movies.DAL.Models;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        MovieDbContext _context = new MovieDbContext();

        [HttpGet("GetAllBill")]
        public List<Payment> GetAllBill()
        {
            return _context.Payments.ToList();
        }

        [HttpGet("GetBillById")]
        public Payment GetById(int id)
        {
            return _context.Payments.Find(id);
        }

        [HttpPost("CreateBill")]
        public ActionResult Create(Payment payment)
        {
            try
            {
                payment.PaymentDate = DateTime.Now;
                _context.Payments.Add(payment);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

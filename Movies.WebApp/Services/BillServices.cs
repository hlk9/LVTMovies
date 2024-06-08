using Movies.DAL.Context;
using Movies.DAL.Models;
using Newtonsoft.Json;

namespace Movies.WebApp.Services
{
    public class BillServices
    {
        MovieDbContext _context;
        HttpClient _httpClient;

        public BillServices()
        {
            _context = new MovieDbContext();
        }

        public List<Payment> GetAll()
        {
            string requestUrl = "https://localhost:7279/api/Bill/GetAllBill";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            List<Payment> lst = JsonConvert.DeserializeObject<List<Payment>>(response);

            return lst;
        }

        public Payment GetById(int id)
        {
            string requestUrl = $"https://localhost:7279/api/Bill/GetBillById?id={id}";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            Payment payment = JsonConvert.DeserializeObject<Payment>(response);
            return payment;
        }

        public bool Create(Payment payment)
        {
            string requestUrl = $"https://localhost:7279/api/Bill/CreateBill";
            var response = _httpClient.PostAsJsonAsync(requestUrl, payment).Result;
            if(response.IsSuccessStatusCode) 
            {
                return true;
            }
            return false;
        }
    }
}

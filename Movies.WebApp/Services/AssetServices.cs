using Azure;
using Movies.DAL.Models;
using Movies.DAL.ViewModels;
using Newtonsoft.Json;

namespace Movies.WebApp.Services
{
    public class AssetServices
    {
        HttpClient _httpClient;
        public AssetServices()
        {
            _httpClient = new HttpClient();
        }
        public List<User> GetAll()
        {
            string requestUrl = "https://localhost:7279/api/User/GetAllUser";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            List<User> lstUsers = JsonConvert.DeserializeObject<List<User>>(response);
            return lstUsers;
        }

        public User GetById(Guid id)
        {
            string requestUrl = $"https://localhost:7279/api/User/GetUserById?id={id}";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            User User = JsonConvert.DeserializeObject<User>(response);

            return User;
        }

        public bool CreateUser(User user)
        {
            string requestUrl = "https://localhost:7279/api/User/CreateUser";
            var response = _httpClient.PostAsJsonAsync(requestUrl, user).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool UpdateUser(User user)
        {
            string requestUrl = "https://localhost:7279/api/User/UpdateUser";
            var response = _httpClient.PutAsJsonAsync(requestUrl, user).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool DeleteUser(Guid id)
        {
            string requestUrl = $"https://localhost:7279/api/User/DeleteUser?id={id}";
            var response = _httpClient.DeleteAsync(requestUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public List<RentalDetail> GellAllRental()
        {
            string requestUrl = $"https://localhost:7279/api/User/list_rented";
            var respone = _httpClient.GetStringAsync(requestUrl).Result;
            List<RentalDetail> lstRantal = JsonConvert.DeserializeObject<List<RentalDetail>>(respone);
            return lstRantal;
        }
    }
}

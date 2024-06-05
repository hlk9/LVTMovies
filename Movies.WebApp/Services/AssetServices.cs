using Azure;
using Movies.DAL.Models;
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
            string requetURl = "https://localhost:7279/api/Asset/GetAllUser";
            var response = _httpClient.GetStringAsync(requetURl).Result;
            List<User> users = JsonConvert.DeserializeObject<List<User>>(response);
            return users;
        }

        public bool CreateUser(User user)
        {
            string requetURl = "https://localhost:7279/api/Asset/Create-User";
            var response = _httpClient.PostAsJsonAsync(requetURl,user).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}

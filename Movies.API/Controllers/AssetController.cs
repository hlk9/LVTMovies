using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Models;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        [HttpGet("GetUserById")]
        public User GetUserByID(Guid id)
        {
            var context = new DAL.Context.MovieDbContext();
            return context.Users.Find(id);
        }

        [HttpGet("GetAllUser")]
        public  ICollection<User> GetAllUser()
        {
            var context = new DAL.Context.MovieDbContext();
            var user = context.Users.ToList();
            return user;
        }

        [HttpPost("Create-User")]
        public ActionResult CreateUser(User user)
        {
            try
            {
                var context = new DAL.Context.MovieDbContext();
                context.Users.Add(user);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }





    }
}

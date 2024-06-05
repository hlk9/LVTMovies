using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Context;
using Movies.DAL.Models;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        MovieDbContext _context = new MovieDbContext();

        [HttpGet("GetAllUser")]
        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        [HttpGet("GetUserById")]
        public User GetById(Guid id)
        {
            return _context.Users.Find(id);
        }

        [HttpPost("CreateUser")]
        public ActionResult CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok("Thêm thành công!!");
            }
            catch
            {
                return BadRequest("Thêm thất bại!!");
            }
        }

        [HttpPut("UpdateUser")]
        public ActionResult EditUser(User user)
        {
            try
            {
                var update = _context.Users.Find(user.Id);

                update.UserName = user.UserName;
                update.FullName = user.FullName;
                update.Status = user.Status;
                update.Email = user.Email;
                update.Password = user.Password;
                update.Gender = user.Gender;

                _context.Users.Update(update);
                _context.SaveChanges();
                return Ok("Sửa thành công!!");
            }
            catch
            {
                return BadRequest("Sửa thất bại!!");
            }
        }

        [HttpDelete("DeleteUser")]
        public ActionResult DeleteUser(Guid id)
        {
            try
            {
                var dele = _context.Users.Find(id);

                _context.Users.Remove(dele);
                _context.SaveChanges();
                return Ok("Xóa thành công");
            }
            catch
            {
                return BadRequest("Xóa thất bại");
            }
        }
    }
}

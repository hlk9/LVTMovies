using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies.API.ViewModel;
using Movies.DAL.Context;
using Movies.DAL.Models;
using Movies.DAL.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        MovieDbContext _context = new MovieDbContext();
        [HttpPost("Login")]
        public ActionResult Login(LoginViewModels lvm)
        {
            
            if (lvm.UserName == null && lvm.Password == null) 
            {
                return BadRequest();
            }
            else
            {
                var acc = _context.Users.FirstOrDefault(x => x.UserName == lvm.UserName && x.Password == lvm.Password);
                if (acc == null)
                    return BadRequest();
                else
                {
                    
                   
                    return Ok("Success");
                }
            }
        }

        
       

        



    }



}

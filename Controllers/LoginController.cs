using Authentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginContextClass _context;
        public LoginController(LoginContextClass context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var data = await _context.logins.ToListAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddData(LoginView loginview)
        {
            var has = new HMACSHA256();
            
            Login login = new Login();
            login.UserName = loginview.UserName;
            login.Email = loginview.Email;
            var pass = has.ComputeHash(Encoding.ASCII.GetBytes(loginview.Password));
            login.Password = pass;
            login.PasswordSalt = has.Key;

            _context.logins.Add(login);
            await _context.SaveChangesAsync();
            
            return Ok(loginview);
        }

        //[HttpPost("APILogin")]
        //public async Task<IActionResult> AddUser(UserLogin userLogin)
        //{
        //    var user = _context.logins.FirstOrDefault(x => x.UserName == userLogin.UserName);
        //    if (user == null)
        //    {
        //        return BadRequest("Please Insert Some Data");
        //    }
        //    var hmac = new HMACSHA256(user.PasswordSalt);
        //    var hmacdata = hmac.ComputeHash(Encoding.ASCII.GetBytes(userLogin.Password));
        //    if (hmacdata.SequenceEqual(user.Password))
        //    {
        //        return Ok("Successfull");
        //    }
        //    else
        //    {
        //        return NotFound("Unsuccessfull");
        //    }
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteData(int id)
        {
            var loginuser = await _context.logins.FindAsync(id);
            if (loginuser == null)
            {
                return NotFound("Data Not Found");
            }

            _context.logins.Remove(loginuser);
            await _context.SaveChangesAsync();

            return Ok("Successfully Deleted");
        }
    }
}

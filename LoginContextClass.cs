using Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication
{
    public class LoginContextClass:DbContext
    {
        public LoginContextClass(DbContextOptions<LoginContextClass> options) : base(options) 
        {

        }

        public DbSet<Login> logins { get; set; } 
    }
}

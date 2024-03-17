using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using todlistApi.Model;

namespace TodolistApi.Context
{
    public class ApplicationDBContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions options):base(options) { }

        public DbSet<Taskk> taskks { get; set; }
       
    }
}

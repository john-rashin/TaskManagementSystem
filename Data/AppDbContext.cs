using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private static AppDbContext? _instance;
        private static readonly object _lock = new();

        public static AppDbContext GetInstance(DbContextOptions<AppDbContext> options)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppDbContext(options);
                    }
                }
            }
            return _instance;
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
    }
}

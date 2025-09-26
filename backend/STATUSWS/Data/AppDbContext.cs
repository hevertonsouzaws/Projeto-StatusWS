using Microsoft.EntityFrameworkCore;
using StatusWS.Models;

namespace StatusWS.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}

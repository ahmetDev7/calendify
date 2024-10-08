using calendify_app.Models;
using Microsoft.EntityFrameworkCore;

namespace calendify.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventAttendance> Event_Attendance { get; set; }        
        public DbSet<User> User { get; set; }
    }
}
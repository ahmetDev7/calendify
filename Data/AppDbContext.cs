using calendify_app.Models;
using Microsoft.EntityFrameworkCore;

namespace calendify.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<calendify_app.Models.Attendance> Attendance { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventAttendance> Event_Attendance { get; set; }
        public DbSet<User> User { get; set; }
        public object AttendanceRequest { get; internal set; }
    }
}
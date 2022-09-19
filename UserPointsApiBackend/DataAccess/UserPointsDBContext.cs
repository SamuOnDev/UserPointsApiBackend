using Microsoft.EntityFrameworkCore;
using UserPointsApiBackend.Models.DataModels;

namespace UserPointsApiBackend.DataAccess
{
    public class UserPointsDBContext : DbContext
    {
        public UserPointsDBContext(DbContextOptions<UserPointsDBContext> options) : base(options)
        {

        }

        public DbSet<User>? Users { get; set; }
        public DbSet<UserPoint> UserPoints { get; set; }
        public DbSet<UserWithPoint> UserWithPoints { get; set; }
    }
}

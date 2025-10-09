using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace Qserp.Models
{
    public class QserpDbContext : DbContext
    {
        public QserpDbContext(DbContextOptions<QserpDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }

    [Table("TestUsers")] 
    public class User
    {
        public int Id { get; set; }
        public required string EMPID { get; set; }
        public required string PASS { get; set; }
        public required int IsAdmin { get; set; }
    }

    public class LoginRequest
    {
        public required string EMPID { get; set; }
        public required string PASS { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using StudentInformation.API.Models;

namespace StudentInformation.API.Data
{
    public class StudentInformationDbContext : DbContext
    {
        public StudentInformationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Students> allStudents { get; set; }
        public DbSet<Departments> departments { get; set; }
        public DbSet<Professors>  professors { get; set; }
    }
}

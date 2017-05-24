using BlueCat.Api.Entity.Entity;
using System.Data.Entity;

namespace BlueCat.Api.Entity
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext()
            : base("name=SchoolDbContext")
        {
        }

        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // to do here!
        }
    }
}

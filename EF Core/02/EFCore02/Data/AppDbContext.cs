using EFCore01.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore01.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<StudCourse> StudCourses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseInst> CourseInsts { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-P61TQNR;Database=EFCore01;Trusted_Connection=True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Fluent API Mappings

            // Course using Fluent API
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");
                entity.HasKey(c => c.ID);
                entity.Property(c => c.Name).HasMaxLength(100).IsRequired();
                entity.Property(c => c.Description).HasMaxLength(200);
            });

            // Course_Inst using Fluent API
            modelBuilder.Entity<CourseInst>(entity =>
            {
                entity.ToTable("Course_Inst");
                entity.HasKey(ci => new { ci.Inst_ID, ci.Course_ID });
                entity.Property(ci => ci.Evaluate).HasMaxLength(50);
            });

            // Department using Fluent API
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");
                entity.HasKey(d => d.ID);
                entity.Property(d => d.Name).HasMaxLength(100).IsRequired();
                entity.Property(d => d.HiringDate).HasColumnType("date");
            });

            #endregion
        }
    }
}

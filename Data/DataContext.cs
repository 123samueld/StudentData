namespace StudentData.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<StudentAndCourse> StudentsAndCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentAndCourse>()
                .HasKey(sac => new { sac.StudentId, sac.CourseId });

            modelBuilder.Entity<StudentAndCourse>()
                .HasOne(s => s.Student)
                .WithMany(sac => sac.StudentsAndCourses)
                .HasForeignKey(s => s.StudentId);

            modelBuilder.Entity<StudentAndCourse>()
                .HasOne(c => c.Course)
                .WithMany(sac => sac.StudentsAndCourses)
                .HasForeignKey(c => c.CourseId);

        }

    }
}

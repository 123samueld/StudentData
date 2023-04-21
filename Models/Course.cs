namespace StudentData.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Degree { get; set; }
        public DateTime EnrolementDate { get; set; }
        public ICollection<StudentAndCourse> StudentsAndCourses { get; set; }
    }
}

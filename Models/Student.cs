namespace StudentData.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int RegistrationNumber { get; set; }
        public ContactInfo? ContactInfo { get; set; } 
        public int CampusId { get; set; }
        public Campus Campus { get; set; }
        public ICollection<StudentAndCourse> StudentsAndCourses { get; set; }
    }
}

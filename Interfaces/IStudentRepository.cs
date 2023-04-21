namespace StudentData.Interfaces
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudents();
        Student GetStudent(int studentId);
        ContactInfo GetContactInfoByStudent(int studentId);
        bool CreateStudent(int courseId, Student student);
        bool UpdateStudent(int courseId, Student student);
        bool DeleteStudent(Student student);
        bool StudentExists(int studentId);
        bool Save();
    }
}

namespace StudentData.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateStudent(int courseId, Student student)
        {
            var studentCourseEntity = _context.Courses.Where(cr => cr.Id == courseId).FirstOrDefault();

            var studentCourse = new StudentAndCourse()
            {
                Student = student,
                Course = studentCourseEntity
            };

            _context.Add(studentCourse);
            _context.Add(student);

            return Save();
        }

        public bool DeleteStudent(Student student)
        {
            _context.Remove(student);
            return Save();
        }

        public ContactInfo GetContactInfoByStudent(int studentId)
        {
            return _context.ContactInfos
                .Where(s => s.StudentId == studentId)
                .FirstOrDefault();
        }

        public Student GetStudent(int studentId)
        {
            return _context.Students
                .Where(s => s.Id == studentId)
                .FirstOrDefault();
        }

        public ICollection<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool StudentExists(int studentId)
        {
            return _context.Students.Any(s => s.Id == studentId);
        }

        public bool UpdateStudent(int studentId, Student student)
        {
            _context.Update(student);
            return Save();
        }
    }
}

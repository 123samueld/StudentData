namespace StudentData.Interfaces
{
    public interface ICourseRepository 
    {
        ICollection<Course> GetCourses();
        Course GetCourse(int courseId);
        bool CreateCourse(Course course);
        bool UpdateCourse(Course course);
        bool DeleteCourse(Course course);
        bool CourseExists(int courseId);
        bool Save();
    }
}

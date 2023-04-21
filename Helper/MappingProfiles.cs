namespace StudentData.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Campus, CampusDto>();
            CreateMap<CampusDto, Campus>();
            CreateMap<ContactInfo, ContactInfoDto>();
            CreateMap<ContactInfoDto, ContactInfo>();
            CreateMap<Course, CourseDto>();
            CreateMap<CourseDto, Course>();
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
        }
    }
}

using StudentData.Models;

namespace StudentData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IMapper _mapper;

        public StudentController(
            IStudentRepository studentRepository, 
            IContactInfoRepository contactInfoRepository, 
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _contactInfoRepository = contactInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetStudents()
        {
            var students = _mapper.Map<List<StudentDto>>(_studentRepository.GetStudents());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(students);
        }

        [HttpGet("{studentId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        [ProducesResponseType(404)]
        public IActionResult GetCampus(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
                return NotFound();
            var student = _mapper.Map<StudentDto>(_studentRepository.GetStudent(studentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(student);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateCampus([FromQuery]int courseId, [FromBody]StudentDto studentCreate)
        {
            if (studentCreate == null)
                return BadRequest(ModelState);

            var student = _studentRepository.GetStudents()
                .Where(s => s.LastName.Trim().ToUpper() == studentCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(student != null)
            {
                ModelState.AddModelError("", "Sorry, this student already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var studentMap = _mapper.Map<Student>(studentCreate);

            if(!_studentRepository.CreateStudent(courseId, studentMap))
            {
                ModelState.AddModelError("", "Sorry, something went wrong while trying to create a new student");
                return StatusCode(500, ModelState);
            }

            return Ok("New student created.");
        }

        [HttpPut("{studentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStudent([FromQuery]int courseId, int studentId, [FromBody] StudentDto studentUpdate)
        {
            if (studentId == null)
                return BadRequest(ModelState);

            if(studentId != studentUpdate.Id)
                return BadRequest(ModelState);

            if (!_studentRepository.StudentExists(studentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var studentMap = _mapper.Map<Student>(studentUpdate);

            if(!_studentRepository.UpdateStudent(courseId, studentMap))
            {
                ModelState.AddModelError("", "Sorry, something went wrong while trying to update this student.");
                return StatusCode(500, ModelState);
            }

            return Ok("Student successfully updated.");
        }

        [HttpDelete("{studentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
                return NotFound();

            var studentToDelete = _studentRepository.GetStudent(studentId);
            var contactInfoToDelete = _contactInfoRepository.GetContactInfoByStudent(studentId);

            if (studentId == null)
                return BadRequest(ModelState);

            if (!_contactInfoRepository.DeleteContactInfo(contactInfoToDelete))
            {
                ModelState.AddModelError("", "Sorry, something went wrong while trying to delete the student.");
                return StatusCode(500, ModelState);
            }

            if (!_studentRepository.DeleteStudent(studentToDelete))
            {
                ModelState.AddModelError("", "Sorry, something went wrong while trying to delete the student.");
                return StatusCode(500, ModelState);
            }


            return Ok("Student and their contact info has been successfully deleted.");
        }
    }
}

using StudentData.Models;

namespace StudentData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Course>))]
        public IActionResult GetCourses()
        {
            var courses = _mapper.Map<List<CourseDto>>(_courseRepository.GetCourses());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(courses);
        }

        [HttpGet("{courseId}")]
        [ProducesResponseType(200, Type = typeof(Course))]
        [ProducesResponseType(404)]
        public IActionResult GetCourse(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
                return NotFound();
            var course = _mapper.Map<CourseDto>(_courseRepository.GetCourse(courseId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(course);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateCourse([FromBody] CourseDto courseCreate)
        {
            if (courseCreate == null)
                return BadRequest(ModelState);

            var course = _courseRepository.GetCourses()
                .Where(c => c.CourseName.Trim().ToUpper() == courseCreate.CourseName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (course != null)
            {
                ModelState.AddModelError("", "Sorry, course already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var courseMap = _mapper.Map<Course>(courseCreate);

            if (!_courseRepository.CreateCourse(courseMap))
            {
                ModelState.AddModelError("", "Sorry, something went wrong while trying to create a new campus");
                return StatusCode(500, ModelState);
            }

            return Ok("New course successfully created.");
        }

        [HttpPut("{courseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCourse(int courseId, [FromBody] CourseDto courseUpdate)
        {
            if (courseId == null)
                return BadRequest(ModelState);

            if (!_courseRepository.CourseExists(courseId))
                return NotFound();

            if(courseId != courseUpdate.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var courseMap = _mapper.Map<Course>(courseUpdate);

            if(!_courseRepository.UpdateCourse(courseMap))
            {
                ModelState.AddModelError("", "Sorry, something went wrong while trying to update this course.");
                return StatusCode(500, ModelState);
            }
            return Ok("Course successfully updated.");
        }

        [HttpDelete("{courseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCourse(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
                return NotFound();

            var courseToDelete = _courseRepository.GetCourse(courseId);

            if (courseId == null)
                return BadRequest(ModelState);

            if (!_courseRepository.DeleteCourse(courseToDelete))
            {
                ModelState.AddModelError("", "Sorry, something went wrong while trying to delete the course.");
                return StatusCode(500, ModelState);
            }

            return Ok("Course deleted.");
        }
    }
}

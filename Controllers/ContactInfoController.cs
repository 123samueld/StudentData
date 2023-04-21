using StudentData.Models;

namespace StudentData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : Controller
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public ContactInfoController(
            IContactInfoRepository contactInfoRepository,
            IStudentRepository studentRepository,
            IMapper mapper)
        {
            _contactInfoRepository = contactInfoRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContactInfo>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllContactInfo()
        {
            var contactInfo = _contactInfoRepository.GetAllContactInfo();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(contactInfo);
        }

        [HttpGet("{studentId}")]
        [ProducesResponseType(200, Type = typeof(ContactInfo))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetContactInfoByStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
                return NotFound();


            var studentsContactInfo = _mapper.Map<ContactInfoDto>(_contactInfoRepository.GetContactInfoByStudent(studentId));

            if (studentsContactInfo == null)
            {
                ModelState.AddModelError("", "Sorry, no contact info exists for this student yet.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(studentsContactInfo);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateCampus([FromQuery] int studentId, [FromBody] ContactInfoDto contactInfoCreate)
        {
            if (contactInfoCreate == null)
                return BadRequest(ModelState);

            if (!_studentRepository.StudentExists(studentId))
                return NotFound();

            var contactInfo = _contactInfoRepository.GetContactInfoByStudent(studentId);

            if (contactInfo != null)
            {
                ModelState.AddModelError("", "Sorry, contact info already exists for this student.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contactInfoMap = _mapper.Map<ContactInfo>(contactInfoCreate);

            if (!_contactInfoRepository.CreateContactInfo(studentId, contactInfoMap))
            {
                ModelState.AddModelError("", "Sorry, something went wrong while trying to create new contact info.");
                return StatusCode(500, ModelState);
            }

            return Ok("New contact info created.");
        }

        [HttpPut("{studentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateContactInfo(int studentId, [FromBody] ContactInfoDto contactInfoUpdate)
        {
            if (studentId == null)
                return BadRequest(ModelState);

            if (studentId != contactInfoUpdate.StudentId)
                return BadRequest(ModelState);

            if (!_studentRepository.StudentExists(studentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contactInfoMap = _mapper.Map<ContactInfo>(contactInfoUpdate);

            if (!_contactInfoRepository.UpdateContactInfo(studentId, contactInfoMap))
            {
                ModelState.AddModelError("", "Sorry, something went wrong while trying to update this contact info.");
                return StatusCode(500, ModelState);
            }

            return Ok("Contact info successfully updated.");
        }
    }
}

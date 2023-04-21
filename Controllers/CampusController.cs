using Microsoft.AspNetCore.Http.HttpResults;

namespace StudentData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampusController : Controller
    {
        private readonly ICampusRepository _campusRepository;
        private readonly IMapper _mapper;

        public CampusController(ICampusRepository campusRepository, IMapper mapper)
        {
            _campusRepository = campusRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Campus>))]
        public IActionResult GetCampuses()
        {
            var campuses = _mapper.Map<List<CampusDto>>(_campusRepository.GetCampuses());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(campuses);
        }

        [HttpGet("{campusId}")]
        [ProducesResponseType(200, Type = typeof(Campus))]
        [ProducesResponseType(404)]
        public IActionResult GetCampus(int campusId)
        {
            if (!_campusRepository.CampusExists(campusId))
                return NotFound();

            var campus = _mapper.Map<CampusDto>(_campusRepository.GetCampus(campusId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(campus);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCampus([FromBody] CampusDto campusCreate)
        {
            if (campusCreate == null)
                return BadRequest(ModelState);

            var campus = _campusRepository.GetCampuses()
                .Where(c => c.CampusName.Trim().ToUpper() == campusCreate.CampusName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(campus != null)
            {
                ModelState.AddModelError("", "Sorry, campus already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var campusMap = _mapper.Map<Campus>(campusCreate);

            if(!_campusRepository.CreateCampus(campusMap))
            {
                ModelState.AddModelError("", "Sorry, something went wrong while trying to create a new campus");
                return StatusCode(500, ModelState);
            }

            return Ok("New campus created.");
        }

        [HttpPut("{campusId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCampus(int campusId,[FromBody] CampusDto campusUpdate)
        {
            if (campusUpdate == null)
                return BadRequest(ModelState);

            if (campusId != campusUpdate.Id)
                return BadRequest(ModelState);

            if (!_campusRepository.CampusExists(campusId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var campusMap = _mapper.Map<Campus>(campusUpdate);

            if(!_campusRepository.UpdateCampus(campusMap))
            {
                ModelState.AddModelError("", "Sorry, something went wrong while trying to update this person.");
                return StatusCode(500, ModelState);
            }

            return Ok("Campus successfully updated.");
        }

        [HttpDelete("{campusId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCampus(int campusId)
        {
            if (!_campusRepository.CampusExists(campusId))
                return NotFound();

            var campusToDelete = _campusRepository.GetCampus(campusId);

            if (campusId == null)
                return BadRequest(ModelState);

            if(!_campusRepository.DeleteCampus(campusToDelete))
            {
                ModelState.AddModelError("", "Sorry, something went wrong while trying to delete the pokemon.");
                return StatusCode(500, ModelState);
            }

            return Ok("Campus deleted.");
        }
    }
}

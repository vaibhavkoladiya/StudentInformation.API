using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformation.API.Data;
using StudentInformation.API.Models;

namespace StudentInformation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorinffoController : Controller
    {
        private readonly StudentInformationDbContext _studentInformationDbContext;

        public ProfessorinffoController(StudentInformationDbContext studentInformationDbContext)
        {
            _studentInformationDbContext = studentInformationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfessor()
        {
            var professor = await _studentInformationDbContext.professors.ToListAsync();

            return Ok(professor);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetProfessor([FromRoute] int id)
        {
            var professor = await _studentInformationDbContext.professors.FirstOrDefaultAsync(x => x.Pid == id);

            if (professor == null)
            {
                return NotFound();
            }

            return Ok(professor);
        }

        [HttpPost]
        public async Task<IActionResult> addProfessor([FromBody] Professors professorReqquest)
        {


            await _studentInformationDbContext.professors.AddAsync(professorReqquest);
            await _studentInformationDbContext.SaveChangesAsync();

            return Ok(professorReqquest);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> updateProfessor([FromRoute] int id, Professors updateProfessorRequest)
        {
            var professor = await _studentInformationDbContext.professors.FindAsync(id);

            if (professor == null)
            {
                return NotFound();
            }

            professor.name = updateProfessorRequest.name;
            professor.email = updateProfessorRequest.email;
            professor.coreSubject = updateProfessorRequest.coreSubject;
            professor.Did = updateProfessorRequest.Did;
            await _studentInformationDbContext.SaveChangesAsync();

            return Ok(professor);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> deleteProfessor([FromRoute] int id)
        {
            var professor = await _studentInformationDbContext.professors.FindAsync(id);

            if (professor == null)
            {
                return NotFound();
            }

            _studentInformationDbContext.Remove(professor);
            await _studentInformationDbContext.SaveChangesAsync();

            return Ok(professor);

        }
    }
}

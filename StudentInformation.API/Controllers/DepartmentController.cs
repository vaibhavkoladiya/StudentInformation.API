using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformation.API.Data;
using StudentInformation.API.Models;

namespace StudentInformation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly StudentInformationDbContext _studentInformationDbContext;

        public DepartmentController(StudentInformationDbContext studentInformationDbContext)
        {
            _studentInformationDbContext = studentInformationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartment()
        {
            var department = await _studentInformationDbContext.departments.ToListAsync();

            return Ok(department);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetDepartment([FromRoute] int id)
        {
            var department = await _studentInformationDbContext.departments.FirstOrDefaultAsync(x => x.Did == id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> addDepartment([FromBody] Departments departmentReqquest)
        {


            await _studentInformationDbContext.departments.AddAsync(departmentReqquest);
            await _studentInformationDbContext.SaveChangesAsync();

            return Ok(departmentReqquest);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> updateStudent([FromRoute] int id, Departments updateDepartmentRequest)
        {
            var Department = await _studentInformationDbContext.departments.FindAsync(id);

            if (Department == null)
            {
                return NotFound();
            }

            Department.departmentName = updateDepartmentRequest.departmentName;
            Department.departmentEmail = updateDepartmentRequest.departmentEmail;
            Department.dphonenumber = updateDepartmentRequest.dphonenumber;
          
            

            await _studentInformationDbContext.SaveChangesAsync();

            return Ok(Department);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> deleteStudent([FromRoute] int id)
        {
            var department = await _studentInformationDbContext.departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            _studentInformationDbContext.Remove(department);
            await _studentInformationDbContext.SaveChangesAsync();

            return Ok(department);

        }
    }
}

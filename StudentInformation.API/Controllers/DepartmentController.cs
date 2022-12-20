using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformation.API.Data;
using StudentInformation.API.Models;
using System.Data;

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

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDepartment()
        {
            var department = await _studentInformationDbContext.departments.ToListAsync();


            return Ok(department);
        }



        [HttpGet()]
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

        //[HttpGet()]
        //[Route("{did:int}")
        [HttpGet("departmentstudents/{did:int}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetstudentDepartment([FromRoute] int did)
        {
            var departmentstu = await _studentInformationDbContext.allStudents.Where(x => x.Did == did).ToListAsync();

            if (departmentstu == null)
            {
                return NotFound();
            }

            return Ok(departmentstu);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> addDepartment([FromBody] Departments departmentReqquest)
        {


            await _studentInformationDbContext.departments.AddAsync(departmentReqquest);
            await _studentInformationDbContext.SaveChangesAsync();

            return Ok(departmentReqquest);

        }

        [HttpPut, Authorize(Roles = "Admin")]
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

        [HttpDelete, Authorize(Roles = "Admin")]
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

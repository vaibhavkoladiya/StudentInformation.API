using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformation.API.Data;
using StudentInformation.API.Models;

namespace StudentInformation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentInffoController : Controller
    {
        private readonly StudentInformationDbContext _studentInformationDbContext;

        public StudentInffoController(StudentInformationDbContext studentInformationDbContext) 
        {
            _studentInformationDbContext = studentInformationDbContext;
        }

        //[HttpGet, Authorize(Roles = "Admin")]
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllStudents()
        {
            var Allstudents = await _studentInformationDbContext.allStudents.ToListAsync();
  

            return Ok(Allstudents); 
        }

        [HttpPost, Authorize(Roles = "Admin")]
        //[HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Students studentReqquest)
        {
            studentReqquest.id = Guid.NewGuid();

            await _studentInformationDbContext.allStudents.AddAsync(studentReqquest);
            await _studentInformationDbContext.SaveChangesAsync();

            return Ok(studentReqquest);

        }

        [HttpGet, Authorize(Roles = "Admin")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetStudent([FromRoute] Guid id)
        {
            var student = await _studentInformationDbContext.allStudents.FirstOrDefaultAsync(x => x.id == id);

           
            
            await _studentInformationDbContext.departments.FirstOrDefaultAsync(x => x.Did == student.Did);

            

            if (student== null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpGet("GetDepartmentOfallStudent"),  Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDepartmentOfallStudent()
        {
            //var departofstudents = await _studentInformationDbContext.allStudents.Where(x => x.Did == did).ToListAsync();

            var Allstudents = await _studentInformationDbContext.allStudents.ToListAsync();
            var departmentNames = new Departments[Allstudents.Count];


            for (int i = 0; i < Allstudents.Count; i++)
            {
                var dep = await _studentInformationDbContext.departments.FirstOrDefaultAsync(x => x.Did == Allstudents[i].Did);



                departmentNames[i] = dep;
               // System.Diagnostics.Debug.WriteLine(departmentNames[i]);


            }

            return Ok(departmentNames);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateStudent([FromRoute] Guid id, Students updateStudentRequest) 
        {
            var student = await _studentInformationDbContext.allStudents.FindAsync(id);

            if(student==null)
            {
                return NotFound();
            }
            
            student.firstName = updateStudentRequest.firstName;
            student.lastName = updateStudentRequest.lastName;
            student.email = updateStudentRequest.email;
            student.phoneNumber = updateStudentRequest.phoneNumber;
            //student.department = updateStudentRequest.department;
            student.gender = updateStudentRequest.gender;
            student.studentId= updateStudentRequest.studentId;
            student.Did = updateStudentRequest.Did;
            await _studentInformationDbContext.SaveChangesAsync();

            return Ok(student);
        }


        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteStudent([FromRoute] Guid id)
        {
            var student = await _studentInformationDbContext.allStudents.FindAsync(id);

            if(student==null)
            {
                return NotFound();
            }

            _studentInformationDbContext.Remove(student);
            await _studentInformationDbContext.SaveChangesAsync();

            return Ok(student);

        }

    }
}

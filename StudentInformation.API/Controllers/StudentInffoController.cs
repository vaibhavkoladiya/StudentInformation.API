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

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllStudents()
        {
            var Allstudents = await _studentInformationDbContext.allStudents.ToListAsync();

           

            for (int i = 0; i < Allstudents.Count; i++)
            {
               await _studentInformationDbContext.departments.FirstOrDefaultAsync(x => x.Did == Allstudents[i].Did);
                
            }

            return Ok(Allstudents); 
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Students studentReqquest)
        {
            studentReqquest.id = Guid.NewGuid();

            await _studentInformationDbContext.allStudents.AddAsync(studentReqquest);
            await _studentInformationDbContext.SaveChangesAsync();

            return Ok(studentReqquest);

        }

        [HttpGet]
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

        [HttpPut]
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


        [HttpDelete]
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

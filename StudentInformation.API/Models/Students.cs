using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentInformation.API.Models
{
    public class Students
    {
        public Guid id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string studentId { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
       
        public long phoneNumber { get; set; }

        [Required]
        //[ForeignKey("Department")]
        public int Did { get; set; }


        //public Departments Department { get; set; }



    }
}

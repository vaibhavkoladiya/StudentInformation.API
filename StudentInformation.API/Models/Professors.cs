using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentInformation.API.Models
{
    public class Professors
    {
        [Key]
        public int Pid { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string coreSubject { get; set; }

        [Required]
        [ForeignKey("Department")]
        public virtual int Did { get; set; }

        
        public virtual Departments Department { get; set; }
    }
}

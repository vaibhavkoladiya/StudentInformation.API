using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentInformation.API.Models
{
    public class Departments
    {
        [Key]
        public int Did { get; set; }
        public string departmentName { get; set; }
        public string departmentEmail { get; set; }
        public long dphonenumber { get; set; }

       





    }
}

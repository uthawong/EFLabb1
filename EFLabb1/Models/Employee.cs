using System.ComponentModel.DataAnnotations;

namespace EFLabb1.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

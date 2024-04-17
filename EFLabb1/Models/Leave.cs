using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFLabb1.Models
{
    public class Leave
    {
        public enum LeaveStatus
        { 
            Pending,
            Approved,
            Denied
        }
        [Key]
        public int LeaveId { get; set; }
        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public LeaveStatus Status { get; set; } = Leave.LeaveStatus.Pending;
        [ForeignKey("Employee")]
        public int FkEmployee { get; set; }
        public Employee? Employee { get; set; }
    }
}

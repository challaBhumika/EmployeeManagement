using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Data
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Salary { get; set; }

        [Required, StringLength(50)]
        public string Location { get; set; }

        [Required, StringLength(100), EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100)]
        public string Department { get; set; }

        [Required, StringLength(100)]
        public string Qualification { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}

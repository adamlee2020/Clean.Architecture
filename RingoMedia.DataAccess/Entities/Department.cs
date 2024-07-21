using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RingoMedia.DataAccess.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public int? ParentDepartmentId { get; set; }

        public Department ParentDepartment { get; set; }
        public ICollection<Department> SubDepartments { get; set; }
    }
}
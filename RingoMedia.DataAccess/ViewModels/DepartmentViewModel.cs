using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RingoMedia.DataAccess.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department name is required.")]
        public string Name { get; set; }
        public IFormFile LogoFile { get; set; }  // File upload
        public string? Logo { get; set; } // File upload
        public int? ParentDepartmentId { get; set; }
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>? Departments { get; set; }
        public List<DepartmentViewModel>? SubDepartments { get; set; }
    }

    public class DepartmentDetailsViewModel
    {
        public DepartmentViewModel Department { get; set; }
        public IEnumerable<DepartmentViewModel> SubDepartments { get; set; }
        public IEnumerable<DepartmentViewModel> ParentDepartments { get; set; }
    }

}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using RingoMedia.App.Validation;
using RingoMedia.DataAccess.ViewModels;
using RingoMedia.Service.Services;
using RingoMedia.UI.Services;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace RingoMedia.UI.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentAppService _departmentAppService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UploadService uploadService;

        public DepartmentController(DepartmentAppService departmentAppService, IHostingEnvironment hostingEnvironment , UploadService uploadService)
        {
            _departmentAppService = departmentAppService;
            _hostingEnvironment = hostingEnvironment;
            this.uploadService = uploadService;
        }
        public IActionResult Index()
        {
            var departments = _departmentAppService.GetAll();
            return View(departments);
        }

        public IActionResult Create()
        {
            var departments =  _departmentAppService.GetAll();
            var viewModel = new DepartmentViewModel();
            // Convert departments to SelectListItem
            viewModel.Departments = departments.Select(d => new SelectListItem 
            { Text = d.Name, Value = d.Id.ToString() }); 
            return View(viewModel);
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.LogoFile != null)
                {
                    viewModel.Logo =  uploadService.UploadFile(viewModel.LogoFile,"upload");
                }
                await _departmentAppService.CreateAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department =  _departmentAppService.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            var viewModel = new DepartmentDetailsViewModel
            {
                Department = department,
                SubDepartments = department.SubDepartments,
                ParentDepartments = await _departmentAppService.GetParentDepartmentsAsync(department.Id)
            };

            return View(viewModel);
        }
    }
}

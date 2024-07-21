using Microsoft.IdentityModel.Tokens;
using RingoMedia.DataAccess.Entities;
using RingoMedia.DataAccess.ViewModels;
using RingoMedia.Service.Services;
using System.ComponentModel.DataAnnotations;

namespace RingoMedia.App.Validation
{
    public class DepartmentAppService
    {
        private readonly DepartmentService Service;

        public DepartmentAppService(DepartmentService service)
        {
            Service = service;
        }

        public async Task<DepartmentViewModel> CreateAsync(DepartmentViewModel model)
        {
            var validationResult = Validate(model);
            if (string.IsNullOrEmpty(validationResult))
            {
                return await Service.CreateDepartmentAsync(model);
            }
            else
            {
                throw new ValidationException(validationResult);
            }
        }

        public DepartmentViewModel GetById(int id)
        {
            if (id >0)
            {
                return Service.GetDepartmentById(id);
            }
            else
            {
                throw new ValidationException("Id is not valid");
            }
        }

        public DepartmentViewModel Update(DepartmentViewModel model)
        {
            var validationResult = Validate(model, true);
            if (string.IsNullOrEmpty(validationResult))
            {
                return Service.CreateDepartmentAsync(model).Result;
            }
            else
            {
                throw new ValidationException(validationResult);
            }
        }

        public async void Delete(int id)
        {
            if (id < 1)
            {
               await Service.DeleteDepartmentAsync(id);
            }
            else
            {
                throw new ValidationException("Id is not valid");
            }
        }

        public List<DepartmentViewModel> GetAll()
        {
            return Service.GetAllDepartmentsAsync().Result.ToList();
        }



        private string Validate(DepartmentViewModel department, bool isUpdate = false)
        {
            // Add validation logic here
            if (department == null)
                return " Department Cannot Be Null";

            if (string.IsNullOrEmpty(department.Name))
                return " Department Name Cannot Be Null Or Empty";

            if (department.Logo!=null && department.Logo.Length==0)
                return " Department Logo Cannot Be Null Or Empty";

            if (isUpdate && department.ParentDepartmentId < 1)
                return " Department Id Not valid";

            return "";
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetParentDepartmentsAsync(int departmentId)
        {
            return await Service.GetParentDepartmentsAsync(departmentId);
        }
    }
}

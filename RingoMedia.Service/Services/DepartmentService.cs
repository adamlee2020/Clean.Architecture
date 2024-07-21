using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RingoMedia.DataAccess.Entities;
using RingoMedia.DataAccess.ViewModels;
using RingoMedia.Infrastructure.Interfaces;

namespace RingoMedia.Service.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<DepartmentViewModel> CreateDepartmentAsync(DepartmentViewModel departmentViewModel)
        {
            var department = _mapper.Map<Department>(departmentViewModel);
            await _departmentRepository.AddAsync(department);
            return _mapper.Map<DepartmentViewModel>(department);
        }

        public async Task<DepartmentViewModel> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            return _mapper.Map<DepartmentViewModel>(department);
        }

        public DepartmentViewModel GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetAll().Include(x => x.SubDepartments).Where(x => x.Id == id)
                .Select(x=> new DepartmentViewModel
                {
                    Id = x.Id,
                    SubDepartments = x.SubDepartments.Select(s => new DepartmentViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        ParentDepartmentId = s.ParentDepartmentId
                    }).ToList(),
                    Logo = x.Logo,
                    Name = x.Name,
                    ParentDepartmentId = x.ParentDepartmentId
                }).First();
            return department;
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetSubDepartmentsAsync(int departmentId)
        {
            var departments = await _departmentRepository.GetAll().Where(x=>x.ParentDepartmentId == departmentId).ToListAsync();
            return _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetParentDepartmentsAsync(int departmentId)
        {
            // Recursive approach  
            List<Department> parentChain = new List<Department>();
            var parentDepartment = await _departmentRepository.GetByIdAsync(departmentId);
            while (parentDepartment?.ParentDepartmentId != null)
            {
                parentDepartment = await _departmentRepository.GetByIdAsync(parentDepartment.ParentDepartmentId.Value);
                parentChain.Add(parentDepartment);
            }
            parentChain.Reverse(); // Display parent hierarchy top-down
            return _mapper.Map<IEnumerable<DepartmentViewModel>>(parentChain); ;
        }
        public async Task UpdateDepartmentAsync(DepartmentViewModel departmentViewModel)
        {
            var department = _mapper.Map<Department>(departmentViewModel);
            await _departmentRepository.UpdateAsync(department);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _departmentRepository.DeleteAsync(id);
        }
    }
}

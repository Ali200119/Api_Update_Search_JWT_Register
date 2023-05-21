using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Employee;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepo,
                               IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(EmployeeCreateDto employee) => await _employeeRepo.CreateAsync(_mapper.Map<Employee>(employee));
      
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync() => _mapper.Map<IEnumerable<EmployeeDto>>(await _employeeRepo.FindAllAsync());

        public async Task<EmployeeDto> GetByIdAsync(int? id) => _mapper.Map<EmployeeDto>(await _employeeRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _employeeRepo.DeleteAsync(await _employeeRepo.GetByIdAsync(id));

        //public async Task UpdateAsync(EmployeeEditDto employee) => await _employeeRepo.UpdateAsync(_mapper.Map<Employee>(employee));

        public async Task UpdateAsync(EmployeeEditDto employee, int? id)
        {
            Employee existedEmployee = await _employeeRepo.GetByIdAsync(id);

            await _employeeRepo.UpdateAsync(_mapper.Map(employee, existedEmployee));
        }

        public async Task<IEnumerable<EmployeeDto>> Search(string seacrhText) => _mapper.Map<IEnumerable<EmployeeDto>>(await _employeeRepo.FindAllAsync(e => e.FullName.ToLower().Trim().Contains(seacrhText.ToLower().Trim())));

        public async Task SoftDeleteAsync(int? id) => await _employeeRepo.SoftDeleteAsync(await _employeeRepo.GetByIdAsync(id));
    }
}
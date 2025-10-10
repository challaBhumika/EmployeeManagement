using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(EmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeModel>> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees.ToListAsync();

            return _mapper.Map<List<EmployeeModel>>(employees);
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);

            return _mapper.Map<EmployeeModel>(employee);
        }

        public async Task<Employee> GetEmployeeEntityAsync(int employeeId)
        {
            return await _context.Employees.FindAsync(employeeId);
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _context.Employees.AnyAsync(e => e.Email.ToLower() == email.ToLower());
        }

        public async Task<EmployeeModel> AddEmployeeAsync(EmployeeModel employeeModel)
        {
            var employee = _mapper.Map<Employee>(employeeModel);
            employee.Email = employeeModel.Email.ToLower();
            employee.CreatedDate = DateTime.Now;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeModel>(employee);
        }

        public async Task<bool> IsEmailExistsAsync(string email, int employeeId)
        {
            return await _context.Employees.AnyAsync(e => e.Email.ToLower() == email.ToLower() && e.Id != employeeId);
        }

        public async Task<EmployeeModel> UpdateEmployeeAsync(Employee employee, EmployeeModel employeeModel)
        {
            _mapper.Map(employeeModel, employee);
            employee.UpdatedDate = DateTime.Now;
            employee.Email = employeeModel.Email.ToLower();

            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeModel>(employee);
        }

        public async Task<EmployeeModel> PatchEmployeeAsync(Employee employee, JsonPatchDocument employeeModel)
        {
            employeeModel.ApplyTo(employee);
            employee.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeModel>(employee);
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
using EmployeeManagement.Data;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace EmployeeManagement.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> GetAllEmployeesAsync();

        Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId);

        Task<Employee> GetEmployeeEntityAsync(int employeeId);

        Task<EmployeeModel> AddEmployeeAsync(EmployeeModel employeeModel);

        Task<bool> IsEmailExistsAsync(string email);

        Task<bool> IsEmailExistsAsync(string email, int employeeId);

        Task<EmployeeModel> UpdateEmployeeAsync(Employee employee, EmployeeModel employeeModel);

        Task<EmployeeModel> PatchEmployeeAsync(Employee employee, JsonPatchDocument employeeModel);

        Task DeleteEmployeeAsync(Employee employee);
    }
}
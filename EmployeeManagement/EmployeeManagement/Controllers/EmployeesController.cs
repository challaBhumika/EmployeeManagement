using EmployeeManagement.Model;
using EmployeeManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();

            if (employees.Count == 0)
            {
                return NotFound("No Employees found");
            }

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (id <= 0)
            {
                return BadRequest("Id should be great than zero");
            }

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] EmployeeModel employeeModel)
        {
            bool emailExist = await _employeeRepository.IsEmailExistsAsync(employeeModel.Email);

            if (emailExist)
            {
                return Conflict("Email Already exists for another employee");
            }

            var employee = await _employeeRepository.AddEmployeeAsync(employeeModel);
            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] EmployeeModel employeeModel)
        {
            var employee = await _employeeRepository.GetEmployeeEntityAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            bool emailExist = await _employeeRepository.IsEmailExistsAsync(employeeModel.Email, id);

            if (emailExist)
            {
                return Conflict("Email Already exists for another employee");
            }

            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employee, employeeModel);
            return Ok(updatedEmployee);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatchAsync([FromRoute] int id, [FromBody] JsonPatchDocument employeeModel)
        {
            var employee = await _employeeRepository.GetEmployeeEntityAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var updatedEmployee = await _employeeRepository.PatchEmployeeAsync(employee, employeeModel);
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var employee = await _employeeRepository.GetEmployeeEntityAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            await _employeeRepository.DeleteEmployeeAsync(employee);
            return Ok("Deleted Successfully");
        }
    }
}
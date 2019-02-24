using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OracleHR.Models.dbModels;
using OracleHR.Repository.repo;

namespace OracleHR.Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IConfiguration _config;
        private EmployeeRepositoryImpl _employeeRepo;

        public EmployeeController(IConfiguration configuration)
        {
            _config = configuration;
            _employeeRepo = new EmployeeRepositoryImpl(_config.GetConnectionString("DbContext"));
        }

        /// <summary>
        /// Get all Employees.
        /// </summary>
        /// <remarks>
        ///  Get all Employees from Oracle HR Database
        /// </remarks>
        /// <returns>A List of Employees</returns>
        /// <response code="200">Success</response>
        [Route("getAllEmployees")]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var results = await _employeeRepo.GetEmployeesAsync();
            return Ok(results);
        }

        /// <summary>
        /// Get a single Employee.
        /// </summary>
        /// <remarks>
        /// Get a single Employee from Oracle HR Database
        /// </remarks>
        /// <returns>A Single Employee</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        /// <param name="employeeId">Employee Id</param>
        [Route("getSingleEmployee/{employeeId}")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<IActionResult> GetSingleEmployee([FromRoute]int employeeId)
        {
            var results = await _employeeRepo.GetEmployeesAsync();
            var employee = results.FirstOrDefault(p => p.EmployeeId == employeeId);
            if (employee == null)
            {
                return NotFound(String.Format("Employee with id {0} not found", employeeId));
            }
            return Ok(employee);
        }
    }
}
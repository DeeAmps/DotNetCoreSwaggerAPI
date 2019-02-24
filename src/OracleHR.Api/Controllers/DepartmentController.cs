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
    [Route("api/departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IConfiguration _config;
        private DepartmentRepositoryImpl _departmentRepo;

        public DepartmentController(IConfiguration configuration)
        {
            _config = configuration;
            _departmentRepo = new DepartmentRepositoryImpl(_config.GetConnectionString("DbContext"));
        }

        /// <summary>
        /// Get all Departments.
        /// </summary>
        /// <remarks>
        ///  Get all Departments from Oracle HR Database
        /// </remarks>
        /// <returns>A List of Departments</returns>
        /// <response code="200">Success</response>
        [Route("getAllDepartments")]
        [ProducesResponseType(typeof(List<Department>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var results = await _departmentRepo.GetDepartmentsAsync();
            return Ok(results);
        }

        /// <summary>
        /// Get a single Department.
        /// </summary>
        /// <remarks>
        /// Get a single Department from Oracle HR Database
        /// </remarks>
        /// <returns>A Single Department</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        /// <param name="departmentId">Department Id</param>
        [Route("getSingleDepartment/{departmentId}")]
        [ProducesResponseType(typeof(Department), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<IActionResult> GetSingleCountry([FromRoute]int departmentId)
        {
            var results = await _departmentRepo.GetDepartmentsAsync();
            var department = results.FirstOrDefault(p => p.DepartmentId == departmentId);
            if (department == null)
            {
                return NotFound(String.Format("Department with id {0} not found", departmentId));
            }
            return Ok(department);
        }
    }
}
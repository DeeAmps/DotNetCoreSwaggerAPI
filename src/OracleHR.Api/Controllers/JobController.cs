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
    [Route("api/jobs")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private IConfiguration _config;
        private JobRepositoryImpl _jobRepo;

        public JobController(IConfiguration configuration)
        {
            _config = configuration;
            _jobRepo = new JobRepositoryImpl(_config.GetConnectionString("DbContext"));
        }

        /// <summary>
        /// Get all Jobs.
        /// </summary>
        /// <remarks>
        ///  Get all Jobs from Oracle HR Database
        /// </remarks>
        /// <returns>A List of Jobs</returns>
        /// <response code="200">Success</response>
        [Route("getAllJobs")]
        [ProducesResponseType(typeof(List<Job>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var results = await _jobRepo.GetJobsAsync();
            return Ok(results);
        }

        /// <summary>
        /// Get a single Job.
        /// </summary>
        /// <remarks>
        /// Get a single Job from Oracle HR Database
        /// </remarks>
        /// <returns>A Single Job</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        /// <param name="jobId">Job Id</param>
        [Route("getSingleJob/{jobId}")]
        [ProducesResponseType(typeof(Job), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<IActionResult> GetSingleJob([FromRoute]string jobId)
        {
            var results = await _jobRepo.GetJobsAsync();
            var job = results.FirstOrDefault(p => p.JobId == jobId);
            if (job == null)
            {
                return NotFound(String.Format("Job with id {0} not found", jobId));
            }
            return Ok(job);
        }
    }
}
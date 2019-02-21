using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OracleHR.Repository.repo;

namespace OracleHR.Api.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private IConfiguration _config;
        private RegionRepositoryImpl _regionRepo;

        public RegionController(IConfiguration configuration)
        {
            _config = configuration;
            _regionRepo = new RegionRepositoryImpl(_config.GetSection("ConnectionStrings").GetSection("DbContext").ToString());
        }

        [Route("getAllRegions")]
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var results = await _regionRepo.GetRegions();
            return Ok(results);
        }
    }
}
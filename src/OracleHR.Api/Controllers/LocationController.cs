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
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private IConfiguration _config;
        private LocationRepositoryImpl _locationRepo;

        public LocationController(IConfiguration configuration)
        {
            _config = configuration;
            _locationRepo = new LocationRepositoryImpl(_config.GetConnectionString("DbContext"));
        }

        /// <summary>
        /// Get all Locations.
        /// </summary>
        /// <remarks>
        ///  Get all Locations from Oracle HR Database
        /// </remarks>
        /// <returns>A List of Locations</returns>
        /// <response code="200">Success</response>
        [Route("getAllLocations")]
        [ProducesResponseType(typeof(List<Location>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var results = await _locationRepo.GetLocationsAsync();
            return Ok(results);
        }

        /// <summary>
        /// Get a single Location.
        /// </summary>
        /// <remarks>
        /// Get a single Location from Oracle HR Database
        /// </remarks>
        /// <returns>A Single Location</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        /// <param name="locationId">Location Id</param>
        [Route("getSingleLocation/{locationId}")]
        [ProducesResponseType(typeof(Location), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<IActionResult> GetSingleLocation([FromRoute]int locationId)
        {
            var results = await _locationRepo.GetLocationsAsync();
            var location = results.FirstOrDefault(p => p.LocationId == locationId);
            if (location == null)
            {
                return NotFound(String.Format("Location with id {0} not found", locationId));
            }
            return Ok(location);
        }
    }
}
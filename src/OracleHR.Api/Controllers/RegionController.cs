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
    [Route("api/regions")]
    [ApiController]
    [Produces("application/json")]
    public class RegionController : ControllerBase
    {
        private IConfiguration _config;
        private RegionRepositoryImpl _regionRepo;

        public RegionController(IConfiguration configuration)
        {
            _config = configuration;
            _regionRepo = new RegionRepositoryImpl(_config.GetConnectionString("DbContext"));
        }

        /// <summary>
        /// Get all Regions.
        /// </summary>
        /// <remarks>
        ///  Get all Regions from Oracle HR Database
        /// </remarks>
        /// <returns>A List of Regions</returns>
        /// <response code="200">Success</response>
        [Route("getAllRegions")]
        [ProducesResponseType(typeof(List<Region>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var results = await _regionRepo.GetRegionsAsync();
            return Ok(results);
        }

        /// <summary>
        /// Get a single Region.
        /// </summary>
        /// <remarks>
        /// Get a single Region from Oracle HR Database
        /// </remarks>
        /// <returns>A Single Region</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        /// <param name="regionId">Region Id</param>
        [Route("getSingleRegion/{regionId}")]
        [ProducesResponseType(typeof(Region), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<IActionResult> GetSingleRegion([FromRoute]int regionId)
        {
            var results = await _regionRepo.GetRegionsAsync();
            var region = results.FirstOrDefault(p => p.RegionId == regionId);
            if (region == null)
            {
                return NotFound(String.Format("Region with id {0} not found", regionId));
            }
            return Ok(region);
        }

        /// <summary>
        /// Add New Region.
        /// </summary>
        /// <remarks>
        /// Add a new Region to Oracle HR Database Region Table
        /// </remarks>
        /// <param name="region"></param>
        /// <response code="201">Created</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [Route("addNewRegion")]
        public async Task<IActionResult> AddRegion([FromBody]Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Region results = await _regionRepo.AddNewRegionAsync(region);
            if (results.RegionId != 0)
            {
                return CreatedAtRoute("GetSingleRegion", new { regionId = region.RegionId }, region);
            }
            return StatusCode(500);

        }

        /// <summary>
        /// Update a Region.
        /// </summary>
        /// <remarks>
        /// Update a particular Region in the Oracle HR Database Region Table
        /// </remarks>
        /// <param name="region"></param>
        /// <param name="regionId">Region Id</param>
        /// <response code="201">Created</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="400">Bad Request</response>
        [HttpPut]
        [Route("updateRegion/{regionId}")]
        public async Task<IActionResult> UpdateRegion([FromBody]Region region, [FromRoute] int regionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Region results = await _regionRepo.UpdateRegionAsync(region, regionId);
            if (results.RegionId != 0)
            {
                return Ok(results);
            }
            return StatusCode(500);
        }

        /// <summary>
        /// Delete a Region.
        /// </summary>
        /// <remarks>
        /// Remove a particular Region in the Oracle HR Database Region Table
        /// </remarks>
        /// <param name="regionId">Region Id</param>
        /// <response code="500">Internal Server Error</response>
        /// <response code="404">Region Deleted</response>
        [Route("removeRegion/{regionId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRegion([FromRoute]int regionId)
        {
            int results = await _regionRepo.DeleteRegionAsync(regionId);
            if (results == 0)
            {
                return BadRequest(String.Format("No Region with id {0}", regionId));
            }
            return NotFound();
        }
    }
}
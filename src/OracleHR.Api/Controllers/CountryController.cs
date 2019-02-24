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
    [Route("api/countries")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private IConfiguration _config;
        private CountryRepositoryImpl _countryRepo;

        public CountryController(IConfiguration configuration)
        {
            _config = configuration;
            _countryRepo = new CountryRepositoryImpl(_config.GetConnectionString("DbContext"));
        }

        /// <summary>
        /// Get all Countries.
        /// </summary>
        /// <remarks>
        ///  Get all Countries from Oracle HR Database
        /// </remarks>
        /// <returns>A List of Countries</returns>
        /// <response code="200">Success</response>
        [Route("getAllCountries")]
        [ProducesResponseType(typeof(List<Country>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var results = await _countryRepo.GetCountriesAsync();
            return Ok(results);
        }

        /// <summary>
        /// Get a single Country.
        /// </summary>
        /// <remarks>
        /// Get a single Country from Oracle HR Database
        /// </remarks>
        /// <returns>A Single Country</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        /// <param name="countryId">Country Id</param>
        [Route("getSingleCountry/{countryId}")]
        [ProducesResponseType(typeof(Country), 200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<IActionResult> GetSingleCountry([FromRoute]string countryId)
        {
            var results = await _countryRepo.GetCountriesAsync();
            var country = results.FirstOrDefault(p => p.CountryId == countryId);
            if (country == null)
            {
                return NotFound(String.Format("Country with id {0} not found", countryId));
            }
            return Ok(country);
        }
    }
}
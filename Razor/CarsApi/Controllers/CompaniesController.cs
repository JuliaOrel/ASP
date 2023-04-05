using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarsApi.Data;
using CarsShared.Models;
using CarsApi.Interfaces;

namespace CarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService )
        {
           
            _companyService = companyService;
        }

        // GET: api/Companies/GetCompanies
        [HttpGet("GetCompanies")]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetCompanies()
        {
            var result = await _companyService.GetCompanies();
            return Ok(result);
        }

        // GET: api/Companies/GetCompaniesDetails
        [HttpGet("GetCompaniesDetails")]
        public async Task<ActionResult<IEnumerable<CompanyDetailsDTO>>> GetCompaniesDetails()
        {
            var result = await _companyService.GetCompaniesDetails();
            return Ok(result);
        }

        // GET: api/Companies/GetCompany/5
        [HttpGet("GetCompany/{id}")]
        public async Task<ActionResult<CompanyDTO>> GetCompany(int id)
        {
            var result = await _companyService.GetCompany(id);
            if(result is null)
            {
                return NotFound();
            }

            return result;
        }

        // GET: api/Companies/GetCompanyDetails/5
        [HttpGet("GetCompanyDetails/{id}")]
        public async Task<ActionResult<CompanyDetailsDTO>> GetCompanyDetails(int id)
        {
            var result = await _companyService.GetCompanyDetails(id);
            if (result is null)
            {
                return NotFound();
            }

            return result;
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<CompanyDTO>> PutCompany(int id, CompanyDTO company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            CompanyDTO result = null; 

            try
            {
                result= await _companyService.PutCompany(company);
                if(result is null)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_companyService.CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return result;
        }

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyDTO>> PostCompany(CompanyDTO company)
        {
            if(company.Id > 0)
            {
                company.Id = 0;
            }
            var result = await _companyService.PostCompany(company);

            return CreatedAtAction("GetCompany", new { id = result.Id }, result);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CompanyDTO>> DeleteCompany(int id)
        {
            var result = await _companyService.DelCompany(id);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

      
    }
}

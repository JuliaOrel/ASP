﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CountriesCities.Data;
using CountriesCities.Data.Entities;
using AutoMapper;
using CountriesCitiesShared.DTO;

namespace CountriesCities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CountriesCitiesContext _context;
        private readonly IMapper _mapper;

        public CitiesController(CountriesCitiesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDetailsDTO>>> GetCity()
        {
             var entities=await _context.Cities
                .Include(c => c.Country)
                .ToListAsync();
            var cities = _mapper.Map<IEnumerable<CityDetailsDTO>>(entities);
            return Ok(cities);
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}

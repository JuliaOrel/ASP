using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarsApi.Data;
using Shared.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarsApiContext _context;

        public CarsController(CarsApiContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
            if (_context.Cars.Any() == false)
            {
                Car car1 = new()
                {
                    Model = "Countryman",
                    Brand = "MINI Cooper",
                    Color = "Chilli Red",
                    Price = 1479151,
                    YearIssue = 2020
                };
                Car car2 = new()
                {
                    Model = "CX-30",
                    Brand = "Mazda",
                    Color = "Magma Red",
                    Price = 1108880,
                    YearIssue = 2020
                };
                Car car3 = new()
                {
                    Model = "T-Roc",
                    Brand = "Volkswagen",
                    Color = "Diamond Metallic",
                    Price = 1212412,
                    YearIssue = 2021
                };
                List<Car> cars = new List<Car>() { car1, car2, car3 };
                _context.Cars.AddRange(cars);
                _context.SaveChanges();
            }
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCar()
        {
            if (_context.Cars == null)
            {
                return NotFound();
            }
            var cars = await _context.Cars.ToListAsync();
            List<CarDTO> dto = cars.Select(t => ToDTO(t)).ToList();
            return Ok(dto);
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDTO>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }
            CarDTO dto = ToDTO(car);
            return dto;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<CarDTO>> PutCar(int id, CarDTO carDto)
        {
            if (id != carDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(ToEntity(carDto)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return carDto;
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarDTO>> PostCar(CarDTO carDTO)
        {
            EntityEntry<Car> entity = _context.Cars.Add(ToEntity(carDTO));
            await _context.SaveChangesAsync();
            carDTO.Id = entity.Entity.Id;               

            return CreatedAtAction("GetCar", new { id = entity.Entity.Id }, carDTO);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarDTO>> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return ToDTO(car);
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

        [NonAction]
        private CarDTO ToDTO(Car car)
        {
            return new CarDTO
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Color = car.Color,
                YearIssue = car.YearIssue,
                Price=car.Price
            };
        }

        [NonAction]
        private Car ToEntity(CarDTO carDTO)
        {
            return new Car
            {
                Id = carDTO.Id,
                Brand = carDTO.Brand,
                Model = carDTO.Model,
                Color = carDTO.Color,
                YearIssue = carDTO.YearIssue,
                Price = carDTO.Price
            };
        }
    }
}

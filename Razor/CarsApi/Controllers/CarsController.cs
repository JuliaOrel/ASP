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
using AutoMapper;
using CarsApi.Interfaces;

namespace CarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarsApiContext _context;
        //private readonly IMapper _mapper;
        private readonly ICarService _carService;

        public CarsController(CarsApiContext context, ICarService carService)
        {
            
            _context = context;
            _carService = carService;
            //_mapper = mapper;
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            Company company1 = new Company
            {
                Name = "BMW"
            };
            Company company2 = new Company
            {
                Name = "Mazda"
            };
            Company company3 = new Company
            {
                Name = "Volkswagen"
            };
            Car car1 = new()
                {
                    Model = "Countryman",
                    Brand = "MINI Cooper",
                    Color = "Chilli Red",
                    Price = 1479151,
                    YearIssue = 2020,
                    Company=company1
                };
                Car car2 = new()
                {
                    Model = "CX-30",
                    Brand = "Mazda",
                    Color = "Magma Red",
                    Price = 1108880,
                    YearIssue = 2020,
                    Company=company2
                };
                Car car3 = new()
                {
                    Model = "T-Roc",
                    Brand = "Volkswagen",
                    Color = "Diamond Metallic",
                    Price = 1212412,
                    YearIssue = 2021,
                    Company=company3
                };
                List<Car> cars = new List<Car>() { car1, car2, car3 };
                _context.Cars.AddRange(cars);
                _context.SaveChanges();
            
        }

        // GET: api/Cars/GetCars
        [HttpGet("GetCars")]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCars()
        {
            if (_context.Cars == null)
            {
                return NotFound();
            }
            //var cars = await _context.Cars.ToListAsync();
            //List<CarDTO> dto = cars.Select(t => ToDTO(t)).ToList();
            //IEnumerable<Car> entities = await _context.Cars
            //.Include(c => c.Company)
            //.ToListAsync();
            //IEnumerable<CarDetailsDTO>cars = _mapper.Map<IEnumerable<CarDetailsDTO>>(entities);
            IEnumerable<CarDTO> cars = await _carService.GetCars();
            return Ok(cars);
        }

        // GET: api/Cars/GetCarsDetails
        [HttpGet("GetCarsDetails")]
        public async Task<ActionResult<IEnumerable<CarDetailsDTO>>> GetCarsDetails()
        {
            if (_context.Cars == null)
            {
                return NotFound();
            }
           
            IEnumerable<CarDetailsDTO> cars = await _carService.GetCarsDetails();
            return Ok(cars);
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

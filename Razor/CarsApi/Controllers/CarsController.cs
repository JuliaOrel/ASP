using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarsApi.Data;
using CarsShared.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using AutoMapper;
using CarsApi.Interfaces;

namespace CarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        //private readonly CarsApiContext _context;
        //private readonly IMapper _mapper;
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            
            //_context = context;
            _carService = carService;
            //_mapper = mapper;               
        }

        // GET: api/Cars/GetCars
        [HttpGet("GetCars")]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCars()
        {
           
            IEnumerable<CarDTO> cars = await _carService.GetCars();
            return Ok(cars);
        }

        // GET: api/Cars/GetCarsDetails
        [HttpGet("GetCarsDetails")]
        public async Task<ActionResult<IEnumerable<CarDetailsDTO>>> GetCarsDetails()
        {
           
            IEnumerable<CarDetailsDTO> cars = await _carService.GetCarsDetails();
            return Ok(cars);
        }
        // GET: api/Cars/GetCar/5
        [HttpGet("GetCar/{id}")]
        public async Task<ActionResult<CarDTO>> GetCar(int id)
        {
            //var car = await _context.Cars.FindAsync(id);
            CarDTO car = await _carService.GetCar(id);

            if (car == null)
            {
                return NotFound();
            }
           
            return car;
        }

        // GET: api/Cars/GetCarDetails/5
        [HttpGet("GetCarDetails/{id}")]
        public async Task<ActionResult<CarDTO>> GetCarDetails(int id)
        {
            
            CarDetailsDTO car = await _carService.GetCarDetails(id);

            if (car == null)
            {
                return NotFound();
            }
            
            return car;
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
            CarDTO result = null;
            //_context.Entry(ToEntity(carDto)).State = EntityState.Modified;

            try
            {
                result = await _carService.PutCar(carDto);
                if(result is null)
                {
                    return NotFound();
                }

                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_carService.CarExists(id))
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

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarDTO>> PostCar(CarDTO carDTO)
        {
            if(carDTO.Id>0)
            {
                carDTO.Id = 0;
            }
            CarDTO result = await _carService.PostCar(carDTO);

            return CreatedAtAction("GetCar", new { id = result.Id }, result);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarDTO>> DeleteCar(int id)
        {
            var result = await _carService.DeleteCar(id);
            if (result is null)
            {
                return NotFound();
            }

            return result;
        }

        //private bool CarExists(int id)
        //{
        //    return _context.Cars.Any(e => e.Id == id);
        //}

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

using AzureTableDoctors.Models;
using AzureTableDoctors.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTableDoctors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly ITableService _tableService;
        public MedicineController(ITableService tableService)
        {
            _tableService = tableService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetMedicines()
        {
            var medicines = await _tableService.GetMedicines();
            return Ok(medicines);
        }

        [HttpGet("{category}")]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetMedicines(string category)
        {
            var medecines = await _tableService.GetMedicines(category);
            return Ok(medecines);
        }

        [HttpPost]
        public async Task<ActionResult<Medicine>> PostMedicine([FromBody] Medicine medicine)
        {
            string id = Guid.NewGuid().ToString();
            medicine.Id = id;
            medicine.RowKey = id;
            medicine.PartitionKey = medicine.Category;
            var createdMedecine = await _tableService.UpsertMedicine(medicine);
            return CreatedAtAction(nameof(GetMedicines), createdMedecine);
        }

        [HttpPut]
        public async Task<ActionResult<Medicine>> PutMedecine([FromBody] Medicine medicine)
        {
            medicine.PartitionKey = medicine.Category;
            medicine.RowKey = medicine.Id;
            var updatedOrAddedMedecine = await _tableService.UpsertMedicine(medicine);
            return Ok(updatedOrAddedMedecine);
        }
        [HttpDelete]
        public async Task<ActionResult<Medicine>> DeleteMedecine(string id, string category)
        {
            await _tableService.DeleteMedicine(id, category);
            return NoContent();
        }
    }
}

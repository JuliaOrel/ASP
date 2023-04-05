using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using CarsShared.Models;
using WebApiQuickOverView.Data;

namespace WebApiQuickOverView.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly WebApiQuickOverViewContext _context;

        public ToDoItemsController(WebApiQuickOverViewContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
            if (_context.ToDoItems.Any() == false)
            {
                ToDoItem toDoItem1 = new()
                {
                    Action = "Feed the cat",
                    Tip = "There is food in the fridge",
                    TimeToComplete = 5,
                    IsCompleted = false,
                    SecretTip = "The cat doesn't eat meat"
                };
                ToDoItem toDoItem2 = new()
                {
                    Action = "Take out the garbage",
                    Tip = null,
                    TimeToComplete = 10,
                    IsCompleted = false,
                    SecretTip = null
                };
                ToDoItem toDoItem3 = new()
                {
                    Action = "Rest",
                    Tip = null,
                    TimeToComplete = 100,
                    IsCompleted = false,
                    SecretTip = null
                };
                List<ToDoItem> toDoItems = new List<ToDoItem>() { toDoItem1, toDoItem2, toDoItem3};
                _context.ToDoItems.AddRange(toDoItems);
                _context.SaveChanges();
            }
                
                    
        }

        // GET: api/ToDoItems
        [HttpGet]
        //[Produces("application/json")] // then we can't use xml
        [FormatFilter]
        public async Task<ActionResult<IEnumerable<ToDoItemDTO>>> GetToDoItem()
        {
            if(_context.ToDoItems==null)
            {
                return NotFound();
            }
            var todoItems= await _context.ToDoItems.ToListAsync();
            List<ToDoItemDTO> dto = todoItems.Select(t => ToDTO(t)).ToList();
           return Ok(dto);
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItemDTO>> GetToDoItem(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound();
            }
            ToDoItemDTO dto = ToDTO(toDoItem);
            return dto;
        }

        // PUT: api/ToDoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoItemDTO>> PutToDoItem(int id, ToDoItemDTO toDoItemDTO)
        {
            if (id != toDoItemDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(ToEntity(toDoItemDTO)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return toDoItemDTO;
        }

        // POST: api/ToDoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoItemDTO>> PostToDoItem(ToDoItemDTO toDoItemDTO)
        {
            EntityEntry<ToDoItem> entity=_context.ToDoItems.Add(ToEntity(toDoItemDTO));
            await _context.SaveChangesAsync();
            toDoItemDTO.Id = entity.Entity.Id;

            return CreatedAtAction("GetToDoItem", new { id = entity.Entity.Id }, toDoItemDTO);
        }

        // DELETE: api/ToDoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoItemDTO>> DeleteToDoItem(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return ToDTO(toDoItem);
        }

        private bool ToDoItemExists(int id)
        {
            return _context.ToDoItems.Any(e => e.Id == id);
        }
        [NonAction]
        private ToDoItemDTO ToDTO(ToDoItem toDo)
        {
            return new ToDoItemDTO
            {
                Id = toDo.Id,
                Action = toDo.Action,
                Tip = toDo.Tip,
                TimeToComplete = toDo.TimeToComplete,
                IsCompleted=toDo.IsCompleted
            };
        }

        [NonAction]
        private ToDoItem ToEntity(ToDoItemDTO toDoItemDTO)
        {
            return new ToDoItem
            {
                Id = toDoItemDTO.Id,
                Action = toDoItemDTO.Action,
                Tip = toDoItemDTO.Tip,
                TimeToComplete = toDoItemDTO.TimeToComplete,
                IsCompleted = toDoItemDTO.IsCompleted
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatsCRUD.Data;
using CatsCRUD.Data.Entities;
using Microsoft.Extensions.Logging;
using CatsCRUD.Models.ViewModels.CatsViewModels;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using CatsCRUD.Models.DTO;
using System.IO;

namespace CatsCRUD.Controllers
{
    public class CatsController : Controller
    {
        private readonly CatsContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CatsController(CatsContext context, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CatsController>();
            _mapper = mapper;
        }

        // GET: Cats
        public async Task<IActionResult> Index(int breedId, string search)
        {
            //var catsContext = _context.Cats.Include(c => c.Breed);
            //return View(await catsContext.ToListAsync());
            IQueryable<Cat> catsIQ = _context.Cats
                .Include(c => c.Breed)
                .Where(c => c.IsDeleted == false);
            if(breedId>0)
            {
                catsIQ = catsIQ.Where(c => c.BreedId == breedId);
            }
            if(search is not null)
            {
                catsIQ = catsIQ.Where(c => c.CatName.Contains(search));
            }
            IQueryable<Breed> breedsIQ = _context.Breeds;
            IEnumerable<BreedDTO> breedDTOs = _mapper.Map<IEnumerable<BreedDTO>>(await breedsIQ.ToListAsync());
            SelectList breedsSL = new SelectList(
                items: breedDTOs,
                dataValueField:"Id",
                dataTextField:"BreedName"
                );
            IndexCatsVM vM = new IndexCatsVM
            {
                Cats =_mapper.Map<IEnumerable<CatDTO>>(await catsIQ.ToListAsync()), //await catsIQ.ToListAsync(),
                BreedSL = breedsSL,
                BreedId = breedId,
                Search = search

            };
            return View(vM);
        }

        // GET: Cats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cats==null)
            {
                return NotFound();
            }

            var cat = await _context.Cats
                .Include(c => c.Breed)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            DetailsCatsVM vM = new DetailsCatsVM
            {
                Cat=_mapper.Map<CatDTO>(cat)//cat
            };
            return View(vM);
        }

        // GET: Cats/Create
        public async Task<IActionResult> CreateAsync()
        {
            IQueryable<Breed> breedsIQ = _context.Breeds;
            IEnumerable<BreedDTO> breedDTOs = _mapper
                .Map<IEnumerable<BreedDTO>>(await breedsIQ.ToListAsync());
            SelectList breedsSL = new SelectList(
               items: breedDTOs,
               dataValueField: "Id",
               dataTextField: "BreedName"
               );
            CreateCatVM vM = new CreateCatVM
            {
                BreedSL = breedsSL
            };
            return View(vM);
        }

        // POST: Cats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCatVM vM)
        {
            if (ModelState.IsValid==false)
            {
                IQueryable<Breed> breedsIQ = _context.Breeds;
                IEnumerable<BreedDTO> breedDTOs = _mapper
                    .Map<IEnumerable<BreedDTO>>(await breedsIQ.ToListAsync());
                SelectList breedsSL = new SelectList(
                   items: breedDTOs,
                   dataValueField: "Id",
                   dataTextField: "BreedName",
                   selectedValue: vM.Cat.BreedId
                   );
                vM.BreedSL = breedsSL;
                foreach (var item in ModelState.Values.SelectMany(e=>e.Errors))
                {
                    _logger.LogError(item.ErrorMessage);
                }
                return View(vM);
            }
            byte[] dataImage = null;
            using(System.IO.BinaryReader br =new BinaryReader(vM.Image.OpenReadStream()))
            {
                dataImage = br.ReadBytes((int)vM.Image.Length);
                vM.Cat.Image = dataImage;
            }
            Cat catToCreate = _mapper.Map<Cat>(vM.Cat);
            _context.Cats.Add(catToCreate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Cats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "BreedName", cat.BreedId);
            return View(cat);
        }

        // POST: Cats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CatName,Description,Gender,Vaccinated,Image,IsDeleted,BreedId")] Cat cat)
        {
            if (id != cat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatExists(cat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "BreedName", cat.BreedId);
            return View(cat);
        }

        // GET: Cats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats
                .Include(c => c.Breed)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        // POST: Cats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cat = await _context.Cats.FindAsync(id);
            _context.Cats.Remove(cat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatExists(int id)
        {
            return _context.Cats.Any(e => e.Id == id);
        }
    }
}

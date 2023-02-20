using ASP_DZ_6_Books.Data;
using ASP_DZ_6_Books.Models;
using ASP_DZ_6_Books.Models.DTO;
using ASP_DZ_6_Books.Models.ViewModels.BooksViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BooksContext _booksContext;
        private readonly IMapper _mapper;

        public HomeController(ILoggerFactory loggerFactory, BooksContext context, IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
            _booksContext = context;
            _mapper = mapper;

        }

        public async Task<ActionResult<IEnumerable<Book>>> IndexAsync(string search)
        {
            IQueryable<Book> books = _booksContext.Books.Where(b=>b.IsDeleted==false);
            if (search is not null)
            {
                books = books.Where(b => b.NameBook.Contains(search));
            }
            //IEnumerable<BookDTO> breedsDTO = _mapper
            //    .Map<IEnumerable<BookDTO>>(await books.ToListAsync());

            IndexBooksVM vM = new IndexBooksVM
            {
                Books = _mapper.Map<IEnumerable<BookDTO>>(await books.ToListAsync()),
                Search = search
            };


            return View(vM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _booksContext.Books == null)
            {
                return NotFound();
            }

            Book book = await _booksContext.Books
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            DetailsBooksVM vM = new DetailsBooksVM
            {
                Book = _mapper.Map<BookDTO>(book)
            };


            return View(vM);
        }

        public async Task<IActionResult> CreateAsync()
        {
            IQueryable<Book> books = _booksContext.Books;

            IEnumerable<BookDTO> booksDTO = _mapper
                .Map<IEnumerable<BookDTO>>(await books.ToListAsync());
            CreateBooksVM vM = new CreateBooksVM();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBooksVM vM)
        {
            if (ModelState.IsValid == false)
            {
                IQueryable<Book> books = _booksContext.Books;

                IEnumerable<BookDTO> booksDTO = _mapper
                    .Map<IEnumerable<BookDTO>>(await books.ToListAsync());


                foreach (var item in ModelState.Values.SelectMany(e => e.Errors))
                {
                    _logger.LogError(item.ErrorMessage);
                }
                return View(vM);

            }
            
            byte[] dataImage = null;
            using (System.IO.BinaryReader br = new BinaryReader(vM.Image.OpenReadStream()))
            {
                dataImage = br.ReadBytes((int)vM.Image.Length);
                vM.Book.Image = dataImage;
            }
            Book bookToCreate = _mapper.Map<Book>(vM.Book);
            _booksContext.Books.Add(bookToCreate);
            await _booksContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _booksContext.Books == null)
            {
                return NotFound();
            }
            Book book = await _booksContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            IQueryable<Book> books = _booksContext.Books;
            
            EditBookVM vM = new EditBookVM
            {
                Book = _mapper.Map<BookDTO>(book),
                
            };
            return View(vM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBookVM vM)
        {
            if (id != vM.Book.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid == false)
            {
                
                return View(vM);
            }
            if (vM.Image is not null)
            {
                byte[] dataImage = null;
                using (System.IO.BinaryReader br = new BinaryReader(vM.Image.OpenReadStream()))
                {
                    dataImage = br.ReadBytes((int)vM.Image.Length);
                    vM.Book.Image = dataImage;
                }
            }

            try
            {
                Book bookToEdit = _mapper.Map<Book>(vM.Book);
                _booksContext.Books.Update(bookToEdit);
                await _booksContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _booksContext.Books == null)
            {
                return NotFound();
            }
            var book = await _booksContext.Books
            .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            DeleteBookVM vM = new DeleteBookVM
            {
                Book = _mapper.Map<BookDTO>(book)
            };
            return View(vM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_booksContext.Books == null)
            {
                return Problem("Entity set 'CatsContext.Cats' is null.");
            }
            Book book = await _booksContext.Books.FindAsync(id);
            if (book != null)
            {
                // _context.Cats.Remove(cat);
                book.IsDeleted = true;
            }
            await _booksContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using ASP_DZ_2_Model.Data;
using ASP_DZ_2_Model.Models;
using ASP_DZ_2_Model.Models.DTO;
using ASP_DZ_2_Model.Models.ViewModels;
using ASP_DZ_2_Model.Models.ViewModels.IndexMoviesVM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_2_Model.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MoviesContext _moviesContext;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, MoviesContext context, IMapper mapper)
        {
            _logger = logger;
            _moviesContext = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {

            return View();
        }
        public async Task<ActionResult<IEnumerable<Movie>>> MovieAsync(int movieId, string search)
        {
            IQueryable<Session> sessionsIQ = _moviesContext.Sessions
                .Include(c => c.Movie)
                .Where(c => c.Movie.IsDeleted == false);
            if (movieId > 0)
            {
                sessionsIQ = sessionsIQ.Where(c => c.MovieId == movieId);
            }
            IQueryable<Movie> moviesIQ = _moviesContext.Movies
                .Include(m => m.Sessions);
            if (search is not null)
            {
                //sessionsIQ = sessionsIQ.Where(c => c.Movie.Name.Contains(search));
                moviesIQ = moviesIQ.Where(m => m.Name.Contains(search));
            }
            
            IEnumerable<MovieDTO> moviesDTOs = _mapper.Map<IEnumerable<MovieDTO>>(await moviesIQ.Include(m=>m.Sessions).ToListAsync());
            SelectList moviessSL = new SelectList(
                items: moviesDTOs,
                dataValueField: "ID",
                dataTextField: "Name"
                );
            IndexMoviesVM vM = new IndexMoviesVM
            {
                Sessions = _mapper.Map<IEnumerable<SessionDTO>>(await sessionsIQ.ToListAsync()),
                Search=search,
                MoviesSL = moviessSL,
                MovieId = movieId,
                Movies= await moviesIQ.ToListAsync()

            };
            return View(vM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _moviesContext.Movies == null)
            {
                return NotFound();
            }
            Movie movie = await _moviesContext.Movies.
            Include(c => c.Sessions)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }
            DetailsMoviesVM vM = new DetailsMoviesVM
            {
                Movie = _mapper.Map<MovieDTO>(movie)
            };
            return View(vM);

        }

        public IActionResult Create()
        {
            return View();
            
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMoviesVM vM)
        {
           
                foreach (var session in vM.Sessions)
                {
                    if(string.IsNullOrEmpty(session.TimeSession))
                    {
                        ModelState.AddModelError("", "Sessions are required");
                    }
                }
                if(ModelState.IsValid==false)
                {
                    return View(vM);
                }

                vM.Movie.Sessions = vM.Sessions;
                await _moviesContext.AddAsync(vM.Movie);
                await _moviesContext.SaveChangesAsync();
               
                return RedirectToAction(nameof(Movie), new { movieId=0, search=""});
          
        }

        // GET: Cats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _moviesContext.Movies.FindAsync(id);

            //if (movie == null || movie.IsDeleted == true)
            //{
            //    return NotFound();
            //}
            IQueryable<Session> sessionsIQ = _moviesContext.Sessions.Where(b => b.MovieId == id);
            IEnumerable<SessionDTO> sessionDTOs = _mapper
                .Map<IEnumerable<SessionDTO>>(await sessionsIQ.ToListAsync());
            //SelectList sessionsSL = new SelectList(
            //   items: sessionDTOs,
            //   dataValueField: "Id",
            //   dataTextField: "TimeSession",
            //   selectedValue: movie.ID);
            //IQueryable<Session> list = _moviesContext.Sessions.Where(b => b.MovieId == id);
            EditMoviesVM vM = new EditMoviesVM
            {
                Movie = _mapper.Map<MovieDTO>(movie),
                //Sessions = (List<Session>)_moviesContext.Sessions.Where(b => b.MovieId == id)
                //Sessions= (List<Session>)sessionDTOs

            };
            //vM.Movie.Sessions = (List<Session>)sessionsIQ;
          
            return View(vM);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditMoviesVM vM)
        {

            foreach (var session in vM.Movie.Sessions)
            {
                if (string.IsNullOrEmpty(session.TimeSession))
                {
                    ModelState.AddModelError("", "Sessions are required");
                }
            }
            if (ModelState.IsValid == false)
            {
                return View(vM);
            }
            vM.Movie.ID = id;
            vM.Movie.Sessions = vM.Sessions;
            Movie movieToEdit = _mapper.Map<Movie>(vM.Movie);
            _moviesContext.Movies.Update(movieToEdit);
            //await _moviesContext.AddAsync(vM.Movie);
            await _moviesContext.SaveChangesAsync();

            return RedirectToAction(nameof(Movie));

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

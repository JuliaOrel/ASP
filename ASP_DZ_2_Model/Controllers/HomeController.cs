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
            if (search is not null)
            {
                sessionsIQ = sessionsIQ.Where(c => c.Movie.Name.Contains(search));
            }
            IQueryable<Movie> moviesIQ = _moviesContext.Movies;
            IEnumerable<MovieDTO> moviesDTOs = _mapper.Map<IEnumerable<MovieDTO>>(await moviesIQ.ToListAsync());
            SelectList moviessSL = new SelectList(
                items: moviesDTOs,
                dataValueField: "ID",
                dataTextField: "Name"
                );
            IndexMoviesVM vM = new IndexMoviesVM
            {
                Sessions= _mapper.Map<IEnumerable<SessionDTO>>(await sessionsIQ.ToListAsync()),
                Search=search,
                MoviesSL = moviessSL,
                MovieId = movieId,
                Movies= moviesIQ

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

        public async Task<IActionResult> Create()
        {
            IQueryable<Session> sessionsIQ = _moviesContext.Sessions;
            IEnumerable<SessionDTO> sessionDTOs = _mapper
                .Map<IEnumerable<SessionDTO>>(await sessionsIQ.ToListAsync());
            SelectList sessionSL = new SelectList(
               items: sessionDTOs,
               dataValueField: "ID",
               dataTextField: "TimeSession"
               );
            CreateMoviesVM vM = new CreateMoviesVM
            {
                SessionsSL= sessionSL,   
            };
            //Movie mov = (Movie)_moviesContext.Movies.Where(n => n.ID == vM.Session.MovieId).Include(g => g.Sessions);
            //vM.Movie = mov;
            //Movie movie = _moviesContext.Movies.Where(v => v.ID == id);
            //vM.Movie.Sessions = (ICollection<Session>)sessionSL;
            return View(vM);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMoviesVM vM)
        {
            if (ModelState.IsValid == false)
            {
                IQueryable<Session> breedsIQ =_moviesContext.Sessions;
                IEnumerable<SessionDTO> breedDTOs = _mapper
                    .Map<IEnumerable<SessionDTO>>(await breedsIQ.ToListAsync());
                SelectList breedsSL = new SelectList(
                   items: breedDTOs,
                   dataValueField: "ID",
                   dataTextField: "TimeSession",
                   selectedValue: vM.Session.ID
                   );
                vM.SessionsSL = breedsSL;
                Movie mov = (Movie)_moviesContext.Movies.Where(n => n.ID == vM.Session.MovieId).Include(g=>g.Sessions);
                vM.Movie = mov;
                //vM.Movie.Sessions = (ICollection<Session>)breedsSL;
                //vM.Movie.Sessions.Add((Session)(ICollection<Session>)breedsSL);
                foreach (var item in ModelState.Values.SelectMany(e => e.Errors))
                {
                    _logger.LogError(item.ErrorMessage);
                }
                return View(vM);
            }

            //IQueryable<Movie> moviesIQ = _moviesContext.Movies;
            //IEnumerable<MovieDTO> movieDTOs = _mapper
            //    .Map<IEnumerable<MovieDTO>>(await moviesIQ.ToListAsync());
            Movie catToCreate = vM.Movie;
            _moviesContext.Movies.Add(catToCreate);
            await _moviesContext.SaveChangesAsync();
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

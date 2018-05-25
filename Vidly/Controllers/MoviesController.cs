using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // to properly dispose _context:
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies/Random
        public ActionResult Random()
        {
            Movie movies = new Movie { Name = "Shrek!" };
            List<Customer> customerList = new List<Customer>
            {
                new Customer {Id=1, Name = "Dani"},
                new Customer {Id=2, Name = "Lili"}
            };

            var randomMvm = new RandomMovieViewModel { Movie = movies, Customers = customerList };

            ViewData["movie"] = movies;
            ViewBag.Movie = movies;

            return View(randomMvm);
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new {page = 1, sortBy = "name"});
        }

        //public ActionResult Index(int pageIndex = 1, string sortBy = "Name")
        //{

        //    return Content("pageIndex="+pageIndex+"&sortBy="+sortBy);
        //}

        //Alternative way to write the Index method:
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue) pageIndex = 1;
        //    if (String.IsNullOrWhiteSpace(sortBy)) sortBy = "Name";
        //    return Content("pageIndex=" + pageIndex + "&sortBy=" + sortBy);
        //}

        // Attribute routing in action:
        // other constraint, eg max, min, minlength, etc
        // a good google search to learn more: "asp.net mvc attribute route constraints"
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year = 0, int month = 1)
        {
            return Content("year: " + year + " month: " + month);
        }

        public ActionResult Index()
        {
            //Movie movies = new Movie { Name = "Shrek!" };
            //List<Movie> movieList = new List<Movie>
            //{
            //    new Movie {Id=1, Name = "Shreck"},
            //    new Movie {Id=2, Name = "Gyalog galopp"}
            //};

            List<Movie> movieList = _context.Movies.Include(x => x.Genre).ToList();

            var mvm = new MovieViewModel { Movies = movieList };
            return View(mvm);
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new {page = 1, sortBy = "name"});
        }

        [Route("Movies/Details/{id}")]
        public ActionResult Details(int id)
        {
            Movie movie = _context.Movies.Include(x => x.Genre).SingleOrDefault(x => x.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            // TODO: for the last movie next button should be inactive...
            //Movie next = _context.Movies.Where(x => x.Id > id).ToList()[0];
            //int nextId = next.Id;

            MovieDetailViewModel cdvm = new MovieDetailViewModel
            {
                Id = movie.Id,
                //NextId = nextId,
                Name = movie.Name,
                GenreName = movie.Genre.GenreName,
                ReleaseDate = movie.ReleaseDate,
                AddedDate = movie.AddedDate,
                NumberInStock = movie.NumberInStock
            };
            return View(cdvm);
        }

        public ActionResult New()
        {
            Movie movie = new Movie{ Id = 0 };
            List<Genre> genres = _context.Genres.ToList();
            MovieFormViewModel mfvm = new MovieFormViewModel();
            mfvm.Movie = movie;
            mfvm.Genres = genres;
            return View("MovieForm", mfvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                MovieFormViewModel mfvm = new MovieFormViewModel();
                mfvm.Movie = movie;
                mfvm.Genres = _context.Genres.ToList();
                return View("MovieForm", mfvm);
            }
            if (movie.Id == 0)
            {
                movie.AddedDate = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(x => x.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movie == null) return HttpNotFound();
            MovieFormViewModel mfvm = new MovieFormViewModel();
            List<Genre> genres = _context.Genres.ToList();
            mfvm.Movie = movie;
            mfvm.Genres = genres;
            return View("MovieForm", mfvm);
        }
    }
}
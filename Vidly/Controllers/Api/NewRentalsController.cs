using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool b)
        {
            _context.Dispose();
        }


        // POST /api/newrentals
        [HttpPost]
        public IHttpActionResult RentMovie(NewRentalDto newRentalDto)
        {
            List<Rental> res = new List<Rental>();
            var customer = _context.Customers.Single(x => x.Id == newRentalDto.CustomerId);
            //int numberOfMovies = newRentalDto.MovieIds.Count;
            //for (int i = 0; i < numberOfMovies; i++)
            // TODO: implement the update the NumberAvailable fileld when a rental happens feature
            // to do this: lekérni movieindb - t (ott van lejjebb, változóba kell kitenni), ezután a movie-nak csökkentsük
            //1-el a NuberAvailable-jét, és a _context.SaveChanges(); meg fog oldani mindent.
            
            // ez egy jó módszer arra, hogy kikeressük a paraméterként átadott objektum MovieId-adik elemében (ami egy lista)
            // szereplő elemeket az adatbázisból:
            //var movies = _context.Movies.Where(x => newRentalDto.MovieIds.Contains(x.Id));

            foreach (var item in newRentalDto.MovieIds)
            {
                var movie = _context.Movies.SingleOrDefault(x => x.Id == item);
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Today
                };
                movie.NumberAvailable--;
                _context.Rentals.Add(rental);
                res.Add(rental);
            }

            _context.SaveChanges();

            return Ok(res);
        }
    }
}

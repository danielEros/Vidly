using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disp)
        {
            _context.Dispose();
        }


        // GET /api/movies
        public IHttpActionResult GetMovies()
        {
            return Ok(_context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>));
        }

        // GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movie == null) return NotFound();
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            Movie movie = Mapper.Map<MovieDto, Movie>(movieDto);
            if (!ModelState.IsValid) return BadRequest();
            movie.AddedDate = DateTime.Now;
            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDto.Id = movie.Id;
            movieDto.AddedDate = movie.AddedDate;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // PUT /api/movies/1
        [HttpPut]
        public IHttpActionResult EditMovie(int id, MovieDto movieDto)
        {
            // TODO: DO NOT MODIFY ADDED DATE!!!
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieInDb = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movieInDb == null) return NotFound();
            Mapper.Map(movieDto, movieInDb);
            _context.SaveChanges();
            return Ok();
        }

        // DELETE /api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movie == null) return NotFound(); //throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return Ok("deleted");
        }
    }
}

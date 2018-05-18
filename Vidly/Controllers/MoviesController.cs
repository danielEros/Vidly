﻿using System;
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

        public ActionResult Edit(int id)
        {
            return Content("id = " + id);
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
            List<Movie> movieList = new List<Movie>
            {
                new Movie {Id=1, Name = "Shreck"},
                new Movie {Id=2, Name = "Gyalog galopp"}
            };

            var mvm = new MovieViewModel { Movies = movieList };
            return View(mvm);
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new {page = 1, sortBy = "name"});
        }
    }
}
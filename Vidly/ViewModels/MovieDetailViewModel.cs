using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieDetailViewModel
    {
        public int Id { get; set; }
        public int NextId { get; set; }
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? AddedDate { get; set; }
        public int NumberInStock { get; set; }
        public string GenreName { get; set; }
    }
}
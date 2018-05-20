using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerDetailViewModel
    {
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string MembershipTypeName { get; set; }
    }
}
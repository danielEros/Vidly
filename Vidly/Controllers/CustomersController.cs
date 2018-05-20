using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // we use this in this way by convention:
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // to properly dispose _context:
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var customerList = _context.Customers.Include(x => x.MembershipType).ToList();
            CustomerViewModel cvm = new CustomerViewModel {Customers = customerList};
            return View(cvm);
        }

        [Route("Customers/Details/{id}")]
        public ActionResult Details(int id)
        {
            // with eager loading we can access the attached tables
            // to do this, use Include(x => x.Membershiptype) in this case
            // and have to manually import "using System.Data.Entity;"
            var customer = _context.Customers.Include(x => x.MembershipType).SingleOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            // this shoulb be done in a more sopisticated manner
            //var membershipType = _context.MembershipTypes.SingleOrDefault(x => x.Id == customer.MembershipTypeId);
            

            CustomerDetailViewModel cdvm = new CustomerDetailViewModel
            {
                Name = customer.Name,
                MembershipTypeName = customer.MembershipType.Name,
                Birthday = customer.Birthday
            };
            return View(cdvm);
        }
    }
}
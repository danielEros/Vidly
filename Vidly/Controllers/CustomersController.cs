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

        public ActionResult New()
        {
            List<MembershipType> membershipTypeList = _context.MembershipTypes.ToList();
            Customer customer = new Customer();
            customer.Id = 0;
            CustomerFormViewModel ncvm = new CustomerFormViewModel();
            ncvm.Customer = customer;
            ncvm.MembershipTypes = membershipTypeList;
            return View("CustomerForm", ncvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerFormViewModel ncvm)
        {
            if (!ModelState.IsValid)
            {
                List<MembershipType> membershipTypes = _context.MembershipTypes.ToList();
                ncvm.MembershipTypes = membershipTypes;
                return View("CustomerForm", ncvm);
            }
            if (ncvm.Customer.Id == 0)
            {
                _context.Customers.Add(ncvm.Customer);
            }
            else
            {
                Customer customerInDb = _context.Customers.Single(x => x.Id == ncvm.Customer.Id);

                // Automapperrel lehetne így:
                // Mapper.Map(ncvm.Customer, customerInDb);
                // de ekkor MINDEN mezőt felülírna, meg lehet oldani ezt is, de most nem fogl.

                customerInDb.Name = ncvm.Customer.Name;
                customerInDb.Birthday = ncvm.Customer.Birthday;
                customerInDb.MembershipTypeId = ncvm.Customer.MembershipTypeId;
                customerInDb.IsSubsrcibedToNewsletter = ncvm.Customer.IsSubsrcibedToNewsletter;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
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

        public ActionResult Edit(int id)
        {
            var cusromer = _context.Customers.Include(x => x.MembershipType).SingleOrDefault(x => x.Id == id);
            if (cusromer == null)
            {
                return HttpNotFound();
            }
            CustomerFormViewModel ncvm = new CustomerFormViewModel
            {
                Customer = cusromer, MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", ncvm);
        }
    }
}
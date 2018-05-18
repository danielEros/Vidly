using System;
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
        public ActionResult Index()
        {
           List<Customer> customerList = new List<Customer>
            {
                //new Customer {Id=1, Name = "Dani"},
                //new Customer {Id=2, Name = "Lili"}
            };

            CustomerViewModel cvm = new CustomerViewModel {Customers = customerList};
            return View(cvm);
        }

        [Route("Customers/Details/{id:range(1,2)}")]
        public ActionResult Details(int id)
        {
            string Name_ = "";
            if (id == 1) Name_ = "Dani";
            else if (id == 2) Name_ = "Lili";
            CustomerDetailViewModel cdvm = new CustomerDetailViewModel{Name=Name_};

            return View(cdvm);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disp)
        {
            _context.Dispose();
        }

        // GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            // the Linq extension method to use the automaper:
            // .Select(Mapper.Map<_inputType_, _outputtype_>)
            return Ok(_context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>));
        }

        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customer == null) return NotFound();//throw new HttpResponseException(HttpStatusCode.NotFound);
            //return Mapper.Map <Customer, CustomerDto> (customer);
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers
        // = add a new customer
        // by convention we return the newly generated item, maybe for the id
        [HttpPost]
        //public CustomerDto CreateCustomer(CustomerDto customerDto)
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();
            }

            Customer customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            //return customerDto;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // PUT /api/customers/1
        // = edit a customer
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerInDb = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customerInDb == null) return NotFound(); //throw new HttpResponseException(HttpStatusCode.NotFound);

            // !!
            Mapper.Map(customerDto, customerInDb);

            // with the mapper we dont need these lines:
            //customerInDb.Name = customer.Name;
            //customerInDb.Birthday = customer.Birthday;
            //customerInDb.IsSubsrcibedToNewsletter = customer.IsSubsrcibedToNewsletter;
            //customerInDb.MembershipTypeId = customer.MembershipTypeId;
            _context.SaveChanges();
            return Ok();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customer == null) return NotFound(); //throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return Ok();
        }
    }
}

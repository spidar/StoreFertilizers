using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;
using System.Collections;
using StoreFertilizers.Models.Paging;
using System.Linq.Dynamic;
using Microsoft.AspNet.Cors;

namespace StoreFertilizers.Controllers
{
    [EnableCors("mypolicy")]
    [Produces("application/json")]
    [Route("api/CustomersAPI")]
    public class CustomersAPIController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCustomersList")]
        public IEnumerable GetCustomersList()
        {
            return _context.Customers;
        }

        // GET: api/CustomersAPI
        [HttpGet]
        public PagedList GetCustomersPaging(string searchtext = "", int page = 1, int pageSize = 50, string sortBy = "", string sortDirection = "asc")
        {
            //sortDirection "asc", "desc"
            var pagedRecord = new PagedList();

            var results = _context.Customers.ToList();
            if (!string.IsNullOrEmpty(searchtext))
            {
                results = results.Where(x =>
                        (!string.IsNullOrEmpty(x.CompanyNumber) && x.CompanyNumber.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.Name) && x.Name.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.Address) && x.Address.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.Phone1) && x.Phone1.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.Fax) && x.Fax.Contains(searchtext))
                    ).ToList();
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                results = results.OrderBy(sortBy + " " + sortDirection).ToList();
            }

            pagedRecord.TotalRecords = results.Count();
            pagedRecord.Content = results.Skip((page - 1) * pageSize).Take(pageSize);
            pagedRecord.CurrentPage = page;
            pagedRecord.PageSize = pageSize;

            return pagedRecord;
        }

        // GET: api/CustomersAPI/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Customer customer = _context.Customers.Single(m => m.CustomerID == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return Ok(customer);
        }

        // PUT: api/CustomersAPI/5
        [HttpPut("{id}")]
        public IActionResult PutCustomer(int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != customer.CustomerID)
            {
                return HttpBadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return HttpNotFound();
                }
                else
                {
                    throw;
                }
            }

            return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        }

        // POST: api/CustomersAPI
        [HttpPost]
        public IActionResult PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.Customers.Add(customer);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetCustomer", new { id = customer.CustomerID }, customer);
        }

        // DELETE: api/CustomersAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Customer customer = _context.Customers.Single(m => m.CustomerID == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Count(e => e.CustomerID == id) > 0;
        }
    }
}
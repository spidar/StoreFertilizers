using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    [Produces("application/json")]
    [Route("api/EmployeesAPI")]
    public class EmployeesAPIController : Controller
    {
        private ApplicationDbContext _context;

        public EmployeesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeesAPI
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees;
        }

        // GET: api/EmployeesAPI/5
        [HttpGet("{id}", Name = "GetEmployee")]
        public IActionResult GetEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Employee employee = _context.Employees.Single(m => m.EmployeeID == id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return Ok(employee);
        }

        // PUT: api/EmployeesAPI/5
        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != employee.EmployeeID)
            {
                return HttpBadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/EmployeesAPI
        [HttpPost]
        public IActionResult PostEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.Employees.Add(employee);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.EmployeeID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetEmployee", new { id = employee.EmployeeID }, employee);
        }

        // DELETE: api/EmployeesAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Employee employee = _context.Employees.Single(m => m.EmployeeID == id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}
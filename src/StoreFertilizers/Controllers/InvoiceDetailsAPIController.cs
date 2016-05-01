using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    [Produces("application/json")]
    [Route("api/InvoiceDetailsAPI")]
    public class InvoiceDetailsAPIController : Controller
    {
        private ApplicationDbContext _context;

        public InvoiceDetailsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceDetailsAPI
        [HttpGet]
        public IEnumerable<InvoiceDetails> GetInvoiceDetails()
        {
            return _context.InvoiceDetails;
        }

        // GET: api/InvoiceDetailsAPI/5
        [HttpGet("{id}", Name = "GetInvoiceDetails")]
        public IActionResult GetInvoiceDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            InvoiceDetails invoiceDetails = _context.InvoiceDetails.Single(m => m.InvoiceDetailsID == id);

            if (invoiceDetails == null)
            {
                return HttpNotFound();
            }

            return Ok(invoiceDetails);
        }

        // PUT: api/InvoiceDetailsAPI/5
        [HttpPut("{id}")]
        public IActionResult PutInvoiceDetails(int id, [FromBody] InvoiceDetails invoiceDetails)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != invoiceDetails.InvoiceDetailsID)
            {
                return HttpBadRequest();
            }

            _context.Entry(invoiceDetails).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceDetailsExists(id))
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

        // POST: api/InvoiceDetailsAPI
        [HttpPost]
        public IActionResult PostInvoiceDetails([FromBody] InvoiceDetails invoiceDetails)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.InvoiceDetails.Add(invoiceDetails);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InvoiceDetailsExists(invoiceDetails.InvoiceDetailsID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetInvoiceDetails", new { id = invoiceDetails.InvoiceDetailsID }, invoiceDetails);
        }

        // DELETE: api/InvoiceDetailsAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteInvoiceDetails(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            InvoiceDetails invoiceDetails = _context.InvoiceDetails.Single(m => m.InvoiceDetailsID == id);
            if (invoiceDetails == null)
            {
                return HttpNotFound();
            }

            _context.InvoiceDetails.Remove(invoiceDetails);
            _context.SaveChanges();

            return Ok(invoiceDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceDetailsExists(int id)
        {
            return _context.InvoiceDetails.Count(e => e.InvoiceDetailsID == id) > 0;
        }
    }
}
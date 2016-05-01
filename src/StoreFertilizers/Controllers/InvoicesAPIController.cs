using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    [Produces("application/json")]
    [Route("api/InvoicesAPI")]
    public class InvoicesAPIController : Controller
    {
        private ApplicationDbContext _context;

        public InvoicesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoicesAPI
        [HttpGet]
        public IEnumerable<Invoice> GetInvoices()
        {
            return _context.Invoices;
        }

        // GET: api/InvoicesAPI
        [HttpGet("api/InvoicesAPI/GetNewInvoice", Name = "GetNewInvoice")]
        public Invoice GetNewInvoice()
        {
            return new Invoice();
        }

        // GET: api/InvoicesAPI/5
        [HttpGet("{id}", Name = "GetInvoice")]
        public IActionResult GetInvoice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Invoice invoice = _context.Invoices.Single(m => m.InvoiceID == id);

            if (invoice == null)
            {
                return HttpNotFound();
            }

            return Ok(invoice);
        }

        // PUT: api/InvoicesAPI/5
        [HttpPut("{id}")]
        public IActionResult PutInvoice(int id, [FromBody] Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != invoice.InvoiceID)
            {
                return HttpBadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/InvoicesAPI
        [HttpPost]
        public IActionResult PostInvoice([FromBody] Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.Invoices.Add(invoice);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InvoiceExists(invoice.InvoiceID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetInvoice", new { id = invoice.InvoiceID }, invoice);
        }

        // DELETE: api/InvoicesAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Invoice invoice = _context.Invoices.Single(m => m.InvoiceID == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            _context.Invoices.Remove(invoice);
            _context.SaveChanges();

            return Ok(invoice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Count(e => e.InvoiceID == id) > 0;
        }
    }
}
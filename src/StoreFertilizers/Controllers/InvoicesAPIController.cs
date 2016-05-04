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

            #region "Try to load all child property
            var invoiceDetails = _context.InvoiceDetails.Where(i => i.InvoiceID == invoice.InvoiceID).ToList();

            foreach (var item in invoiceDetails)
            {
                var invoiceDetailsProduct = _context.Products.Where(i => i.ProductID == item.ProductID).ToList();
                var unitType = _context.UnitTypes.Where(i => i.UnitTypeID == item.UnitTypeID).ToList();
                var productType = _context.ProductTypes.Where(i => i.ProductTypeID == item.Product.ProductTypeID).ToList();
                var unitType2 = _context.UnitTypes.Where(i => i.UnitTypeID == item.Product.UnitTypeID).ToList();
            }

            if (invoice.EmployeeID != null)
            {
                invoice.Employee = _context.Employees.SingleOrDefault(i => i.EmployeeID == invoice.EmployeeID);
            }
            if (invoice.CustomerID != null)
            {
                invoice.Customer = _context.Customers.SingleOrDefault(i => i.CustomerID == invoice.CustomerID);
            }
            if (invoice.PaymentTypeID != null)
            {
                invoice.PaymentType = _context.PaymentTypes.SingleOrDefault(i => i.PaymentTypeID == invoice.PaymentTypeID);
            }
            if (invoice.BankID != null)
            {
                invoice.Bank = _context.Banks.SingleOrDefault(i => i.BankID == invoice.BankID);
            }
            #endregion

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

            //var modifiedEntries = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).Select(x => x.Entity).ToList();
            //var addEntries = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).Select(x => x.Entity).ToList();
            //var deletedEntries = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted).Select(x => x.Entity).ToList();

            _context.Entry(invoice).State = EntityState.Modified;

            #region "Handle Add detailInvoices"
            var addNews = invoice.InvoiceDetails.Where(i => i.InvoiceDetailsID == 0).ToList();
            foreach(var item in addNews)
            {
                #region "Handle Stock"
                
                #endregion
                invoice.InvoiceDetails.Remove(item);
                _context.InvoiceDetails.Add(item);
            }
            #endregion
            #region "Handle Edit and Delete"
            foreach (var item in invoice.InvoiceDetails)
            {
                if (item.InvoiceDetailsID > 0)
                {
                    bool isDeleted = (item.IsDeleted != null && item.IsDeleted.Value);
                    if (isDeleted)
                    {
                        _context.InvoiceDetails.Remove(item);
                    }
                    else
                    {
                        _context.Entry(item).State = EntityState.Modified;
                    }
                }
            }
            #endregion

            
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
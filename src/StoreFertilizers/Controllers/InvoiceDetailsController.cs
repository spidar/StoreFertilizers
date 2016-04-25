using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    public class InvoiceDetailsController : Controller
    {
        private ApplicationDbContext _context;

        public InvoiceDetailsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: InvoiceDetails
        public IActionResult Index()
        {
            var applicationDbContext = _context.InvoiceDetails.Include(i => i.Invoice);
            return View(applicationDbContext.ToList());
        }

        // GET: InvoiceDetails/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            InvoiceDetails invoiceDetails = _context.InvoiceDetails.Single(m => m.InvoiceDetailsID == id);
            if (invoiceDetails == null)
            {
                return HttpNotFound();
            }

            return View(invoiceDetails);
        }

        // GET: InvoiceDetails/Create
        public IActionResult Create()
        {
            ViewData["InvoiceID"] = new SelectList(_context.Invoices, "InvoiceID", "Invoice");
            return View();
        }

        // POST: InvoiceDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InvoiceDetails invoiceDetails)
        {
            if (ModelState.IsValid)
            {
                _context.InvoiceDetails.Add(invoiceDetails);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["InvoiceID"] = new SelectList(_context.Invoices, "InvoiceID", "Invoice", invoiceDetails.InvoiceID);
            return View(invoiceDetails);
        }

        // GET: InvoiceDetails/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            InvoiceDetails invoiceDetails = _context.InvoiceDetails.Single(m => m.InvoiceDetailsID == id);
            if (invoiceDetails == null)
            {
                return HttpNotFound();
            }
            ViewData["InvoiceID"] = new SelectList(_context.Invoices, "InvoiceID", "Invoice", invoiceDetails.InvoiceID);
            return View(invoiceDetails);
        }

        // POST: InvoiceDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InvoiceDetails invoiceDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Update(invoiceDetails);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["InvoiceID"] = new SelectList(_context.Invoices, "InvoiceID", "Invoice", invoiceDetails.InvoiceID);
            return View(invoiceDetails);
        }

        // GET: InvoiceDetails/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            InvoiceDetails invoiceDetails = _context.InvoiceDetails.Single(m => m.InvoiceDetailsID == id);
            if (invoiceDetails == null)
            {
                return HttpNotFound();
            }

            return View(invoiceDetails);
        }

        // POST: InvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            InvoiceDetails invoiceDetails = _context.InvoiceDetails.Single(m => m.InvoiceDetailsID == id);
            _context.InvoiceDetails.Remove(invoiceDetails);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    public class InvoicesController : Controller
    {
        private ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Invoices
        public IActionResult Index()
        {
            var applicationDbContext = _context.Invoices.Include(i => i.Customer);
            return View(applicationDbContext.ToList());
        }

        // GET: Invoices/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Invoice invoice = _context.Invoices.Single(m => m.InvoiceID == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Name");
            return View();
        }

        // POST: Invoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Invoices.Add(invoice);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Customer", invoice.CustomerID);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Invoice invoice = _context.Invoices.Single(m => m.InvoiceID == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Customer", invoice.CustomerID);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Update(invoice);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Customer", invoice.CustomerID);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Invoice invoice = _context.Invoices.Single(m => m.InvoiceID == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = _context.Invoices.Single(m => m.InvoiceID == id);
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

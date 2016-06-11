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
            //var applicationDbContext = _context.Invoices.Include(i => i.Bank).Include(i => i.Customer).Include(i => i.Employee).Include(i => i.PaymentType);
            //return View(applicationDbContext.ToList());
            return View(null);
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
        public IActionResult Create(bool? isTax)
        {
            if (isTax != null)
            {
                ViewData["isTax"] = isTax.Value;
            }
            //ViewData["BankID"] = new SelectList(_context.Banks, "BankID", "Name");
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Name");
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Name");
            //ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "PaymentTypeID", "Name");
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
            //ViewData["BankID"] = new SelectList(_context.Banks, "BankID", "Name", invoice.BankID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Name", invoice.CustomerID);
            //ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Name", invoice.EmployeeID);
            //ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "PaymentTypeID", "Name", invoice.PaymentTypeID);
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
            //ViewData["BankID"] = new SelectList(_context.Banks, "BankID", "Name", invoice.BankID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Name", invoice.CustomerID);
            //ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Name", invoice.EmployeeID);
            //ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "PaymentTypeID", "Name", invoice.PaymentTypeID);
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
            //ViewData["BankID"] = new SelectList(_context.Banks, "BankID", "Name", invoice.BankID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Name", invoice.CustomerID);
            //ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Name", invoice.EmployeeID);
            //ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "PaymentTypeID", "Name", invoice.PaymentTypeID);
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

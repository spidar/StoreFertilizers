using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    public class SalesSummaryController : Controller
    {
        private ApplicationDbContext _context;

        public SalesSummaryController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: SalesSummary
        public IActionResult Index()
        {
            var applicationDbContext = _context.Invoices.Include(i => i.Bank).Include(i => i.Customer).Include(i => i.Employee).Include(i => i.PaymentType);
            return View(applicationDbContext.ToList());
        }

        // GET: SalesSummary/Details/5
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

        // GET: SalesSummary/Create
        public IActionResult Create()
        {
            //ViewData["BankID"] = new SelectList(_context.Banks, "BankID", "Bank");
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Customer");
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Employee");
            //ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "PaymentTypeID", "PaymentType");
            return View();
        }

        // POST: SalesSummary/Create
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
            //ViewData["BankID"] = new SelectList(_context.Banks, "BankID", "Bank", invoice.BankID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Customer", invoice.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Employee", invoice.EmployeeID);
            //ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "PaymentTypeID", "PaymentType", invoice.PaymentTypeID);
            return View(invoice);
        }

        // GET: SalesSummary/Edit/5
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
            //ViewData["BankID"] = new SelectList(_context.Banks, "BankID", "Bank", invoice.BankID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Customer", invoice.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Employee", invoice.EmployeeID);
            //ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "PaymentTypeID", "PaymentType", invoice.PaymentTypeID);
            return View(invoice);
        }

        // POST: SalesSummary/Edit/5
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
            //ViewData["BankID"] = new SelectList(_context.Banks, "BankID", "Bank", invoice.BankID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Customer", invoice.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Employee", invoice.EmployeeID);
            //ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "PaymentTypeID", "PaymentType", invoice.PaymentTypeID);
            return View(invoice);
        }

        // GET: SalesSummary/Delete/5
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

        // POST: SalesSummary/Delete/5
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

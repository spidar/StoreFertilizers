using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    public class PurchasesController : Controller
    {
        private ApplicationDbContext _context;

        public PurchasesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Purchases
        public IActionResult Index()
        {
            var applicationDbContext = _context.Purchases.Include(p => p.Product).Include(p => p.Provider);
            return View(applicationDbContext.ToList());
        }

        // GET: Purchases/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Purchase purchase = _context.Purchases.Single(m => m.PurchaseID == id);
            if (purchase == null)
            {
                return HttpNotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Product");
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "Provider");
            return View();
        }

        // POST: Purchases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _context.Purchases.Add(purchase);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Product", purchase.ProductID);
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "Provider", purchase.ProviderID);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Purchase purchase = _context.Purchases.Single(m => m.PurchaseID == id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Product", purchase.ProductID);
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "Provider", purchase.ProviderID);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _context.Update(purchase);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Product", purchase.ProductID);
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "Provider", purchase.ProviderID);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Purchase purchase = _context.Purchases.Single(m => m.PurchaseID == id);
            if (purchase == null)
            {
                return HttpNotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = _context.Purchases.Single(m => m.PurchaseID == id);
            _context.Purchases.Remove(purchase);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

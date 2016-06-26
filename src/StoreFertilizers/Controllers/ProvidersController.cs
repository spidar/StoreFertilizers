using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    public class ProvidersController : Controller
    {
        private ApplicationDbContext _context;

        public ProvidersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Providers
        public IActionResult Index()
        {
            //return View(_context.Providers.ToList());
            return View(null);
        }

        // GET: Providers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Provider provider = _context.Providers.Single(m => m.ProviderID == id);
            if (provider == null)
            {
                return HttpNotFound();
            }

            return View(provider);
        }

        // GET: Providers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Provider provider)
        {
            if (ModelState.IsValid)
            {
                _context.Providers.Add(provider);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(provider);
        }

        // GET: Providers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Provider provider = _context.Providers.Single(m => m.ProviderID == id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // POST: Providers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Provider provider)
        {
            if (ModelState.IsValid)
            {
                _context.Update(provider);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(provider);
        }

        // GET: Providers/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Provider provider = _context.Providers.Single(m => m.ProviderID == id);
            if (provider == null)
            {
                return HttpNotFound();
            }

            return View(provider);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Provider provider = _context.Providers.Single(m => m.ProviderID == id);
            _context.Providers.Remove(provider);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("conflicted with the REFERENCE"))
                {
                    return HttpBadRequest("ข้อมูลถูกใช้งานโดยรายการซื้อบางตัวไม่สามารถลบได้");
                }
                else
                {
                    return HttpBadRequest(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

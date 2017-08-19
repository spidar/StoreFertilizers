using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    public class PromotionsController : Controller
    {
        private ApplicationDbContext _context;

        public PromotionsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Promotions
        public IActionResult Index()
        {
            return View(_context.Promotions.ToList());
        }

        // GET: Promotions/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Promotion promotion = _context.Promotions.Single(m => m.PromotionID == id);
            if (promotion == null)
            {
                return HttpNotFound();
            }

            return View(promotion);
        }

        // GET: Promotions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promotions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                _context.Promotions.Add(promotion);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promotion);
        }

        // GET: Promotions/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Promotion promotion = _context.Promotions.Single(m => m.PromotionID == id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // POST: Promotions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                _context.Update(promotion);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promotion);
        }

        // GET: Promotions/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Promotion promotion = _context.Promotions.Single(m => m.PromotionID == id);
            if (promotion == null)
            {
                return HttpNotFound();
            }

            return View(promotion);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Promotion promotion = _context.Promotions.Single(m => m.PromotionID == id);
            _context.Promotions.Remove(promotion);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

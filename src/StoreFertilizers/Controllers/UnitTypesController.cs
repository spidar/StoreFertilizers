using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    public class UnitTypesController : Controller
    {
        private ApplicationDbContext _context;

        public UnitTypesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: UnitTypes
        public IActionResult Index()
        {
            return View(_context.UnitTypes.ToList());
        }

        // GET: UnitTypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            UnitType unitType = _context.UnitTypes.Single(m => m.UnitTypeID == id);
            if (unitType == null)
            {
                return HttpNotFound();
            }

            return View(unitType);
        }

        // GET: UnitTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnitTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UnitType unitType)
        {
            if (ModelState.IsValid)
            {
                _context.UnitTypes.Add(unitType);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(unitType);
        }

        // GET: UnitTypes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            UnitType unitType = _context.UnitTypes.Single(m => m.UnitTypeID == id);
            if (unitType == null)
            {
                return HttpNotFound();
            }
            return View(unitType);
        }

        // POST: UnitTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UnitType unitType)
        {
            if (ModelState.IsValid)
            {
                _context.Update(unitType);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(unitType);
        }

        // GET: UnitTypes/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            UnitType unitType = _context.UnitTypes.Single(m => m.UnitTypeID == id);
            if (unitType == null)
            {
                return HttpNotFound();
            }

            return View(unitType);
        }

        // POST: UnitTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            UnitType unitType = _context.UnitTypes.Single(m => m.UnitTypeID == id);
            _context.UnitTypes.Remove(unitType);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

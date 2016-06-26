using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _context;

        public ProductTypesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ProductTypes
        public IActionResult Index()
        {
            return View(_context.ProductTypes.ToList());
        }

        // GET: ProductTypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProductType productType = _context.ProductTypes.Single(m => m.ProductTypeID == id);
            if (productType == null)
            {
                return HttpNotFound();
            }

            return View(productType);
        }

        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _context.ProductTypes.Add(productType);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProductType productType = _context.ProductTypes.Single(m => m.ProductTypeID == id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _context.Update(productType);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProductType productType = _context.ProductTypes.Single(m => m.ProductTypeID == id);
            if (productType == null)
            {
                return HttpNotFound();
            }

            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            ProductType productType = _context.ProductTypes.Single(m => m.ProductTypeID == id);
            _context.ProductTypes.Remove(productType);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("conflicted with the REFERENCE"))
                {
                    return HttpBadRequest("ข้อมูลถูกใช้งานโดยสินค้าบางตัวไม่สามารถลบได้");
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

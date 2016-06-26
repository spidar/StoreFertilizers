using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;
using System.Net.Http.Headers;
using System.IO;
using System;

namespace StoreFertilizers.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Products
        public IActionResult Index()
        {
            //var applicationDbContext = _context.Products.Include(p => p.ProductType).Include(p => p.UnitType);
            //return View(applicationDbContext.ToList());
            return View(null);
        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Product product = _context.Products.Single(m => m.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "Name");
            ViewData["UnitTypeID"] = new SelectList(_context.UnitTypes, "UnitTypeID", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var file = HttpContext.Request.Form.Files.GetFile("productImageUploaded");

                    if (file != null && file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        var reader = new BinaryReader(file.OpenReadStream());
                        product.ProductImage = reader.ReadBytes((int)file.Length);
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }

                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "Name", product.ProductTypeID);
            ViewData["UnitTypeID"] = new SelectList(_context.UnitTypes, "UnitTypeID", "Name", product.UnitTypeID);
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Product product = _context.Products.Single(m => m.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "Name", product.ProductTypeID);
            ViewData["UnitTypeID"] = new SelectList(_context.UnitTypes, "UnitTypeID", "Name", product.UnitTypeID);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var file = HttpContext.Request.Form.Files.GetFile("productImageUploaded");

                    if (file != null && file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        var reader = new BinaryReader(file.OpenReadStream());
                        product.ProductImage = reader.ReadBytes((int)file.Length);
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }

                _context.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "Name", product.ProductTypeID);
            ViewData["UnitTypeID"] = new SelectList(_context.UnitTypes, "UnitTypeID", "Name", product.UnitTypeID);
            return View(product);
        }

        // GET: Products/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Product product = _context.Products.Single(m => m.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Product product = _context.Products.Single(m => m.ProductID == id);
            _context.Products.Remove(product);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("conflicted with the REFERENCE"))
                {
                    return HttpBadRequest("ข้อมูลถูกใช้งานอยู่ไม่สามารถลบได้");
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

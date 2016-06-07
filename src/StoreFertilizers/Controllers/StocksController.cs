using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace StoreFertilizers.Controllers
{
    public class StocksController : Controller
    {
        private ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Stocks
        public IActionResult Index()
        {
            //var applicationDbContext = _context.Stocks.Include(s => s.Product).Include(i => i.Product.UnitType);
            //return View(applicationDbContext.ToList());
            return View(null);
        }

        // GET: Stocks/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Stock stock = _context.Stocks.Single(m => m.StockID == id);
            if (stock == null)
            {
                return HttpNotFound();
            }

            return View(stock);
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name");
            return View();
        }

        // POST: Stocks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Stock stock)
        {
            if (ModelState.IsValid)
            {
                /*
                try
                {
                    var file = HttpContext.Request.Form.Files.GetFile("productImageUploaded");

                    if (file != null && file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        var reader = new BinaryReader(file.OpenReadStream());
                        stock.ProductImage = reader.ReadBytes((int)file.Length);
                    }
                }catch(Exception ex)
                {
                    var error = ex.Message;
                }
                */
                stock.LastUpdated = DateTime.Now;
                _context.Stocks.Add(stock);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", stock.ProductID);
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Stock stock = _context.Stocks.Single(m => m.StockID == id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", stock.ProductID);
            return View(stock);
        }

        // POST: Stocks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Stock stock)
        {
            if (ModelState.IsValid)
            {
                /*
                try
                {
                    var file = HttpContext.Request.Form.Files.GetFile("productImageUploaded");

                    if (file != null && file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        var reader = new BinaryReader(file.OpenReadStream());
                        stock.ProductImage = reader.ReadBytes((int)file.Length);
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
                */
                stock.LastUpdated = DateTime.Now;
                _context.Update(stock);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", stock.ProductID);
            return View(stock);
        }

        // GET: Stocks/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Stock stock = _context.Stocks.Single(m => m.StockID == id);
            if (stock == null)
            {
                return HttpNotFound();
            }

            var product = _context.Products.Where(i => i.ProductID == stock.ProductID).ToList();

            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Stock stock = _context.Stocks.Single(m => m.StockID == id);
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }        
    }
}

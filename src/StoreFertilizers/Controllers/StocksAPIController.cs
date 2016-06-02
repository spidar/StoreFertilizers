using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;
using StoreFertilizers.Models.Paging;
using System.Linq.Dynamic;
using System;

namespace StoreFertilizers.Controllers
{
    [Produces("application/json")]
    [Route("api/StocksAPI")]
    public class StocksAPIController : Controller
    {
        private ApplicationDbContext _context;

        public StocksAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StocksAPI
        [HttpGet("AllStocks")]
        public IEnumerable<Stock> GetStock()
        {
            return _context.Stocks;
        }

        public PagedList GetStocksPaging(string searchtext = "", int productTypeID = 0, int page = 1, int pageSize = 50, string sortBy = "", string sortDirection = "asc")
        {
            //sortDirection "asc", "desc"
            var pagedRecord = new PagedList();

            var stock_result = _context.Stocks.Include(products => products.Product)
                .Select(stock => new
                {
                    StockID = stock.StockID,
                    ProductID = stock.ProductID,
                    Product = stock.Product,
                    ProductNumber = stock.Product.ProductNumber,
                    ProductName = stock.Product.Name,
                    ProductTypeID = stock.Product.ProductType.ProductTypeID,
                    ProductUnitTypeName = stock.Product.UnitType.Name,
                    Location = stock.Location,
                    Balance = stock.Balance,
                    FullCapStock = stock.FullCapStock,
                    LowCapStock = stock.LowCapStock,
                    AlertLowStock = stock.AlertLowStock,
                    LastUpdated = stock.LastUpdated,
                    Notes = stock.Notes
                });
            if (productTypeID > 0)
            {
                stock_result = stock_result.Where(x => x.ProductTypeID == productTypeID);
            }
            if (!string.IsNullOrEmpty(searchtext))
            {
                stock_result = stock_result.Where(x =>
                        (!string.IsNullOrEmpty(x.ProductNumber) && x.ProductNumber.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.ProductName) && x.ProductName.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.Location) && x.Location.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.Notes) && x.Notes.Contains(searchtext))
                    );
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                stock_result = stock_result.OrderBy(sortBy + " " + sortDirection);
            }

            pagedRecord.TotalRecords = stock_result.Count();
            pagedRecord.Content = stock_result.Skip((page - 1) * pageSize).Take(pageSize);
            pagedRecord.CurrentPage = page;
            pagedRecord.PageSize = pageSize;

            return pagedRecord;
        }

        // GET: api/StocksAPI/5
        [HttpGet("{id}", Name = "GetStock")]
        public IActionResult GetStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Stock stock = _context.Stocks.Single(m => m.StockID == id);

            if (stock == null)
            {
                return HttpNotFound();
            }

            return Ok(stock);
        }

        // PUT: api/StocksAPI/5
        [HttpPut("{id}")]
        public IActionResult PutStock(int id, [FromBody] Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != stock.StockID)
            {
                return HttpBadRequest();
            }
            stock.LastUpdated = DateTime.Now;
            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
                {
                    return HttpNotFound();
                }
                else
                {
                    throw;
                }
            }

            return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        }

        // POST: api/StocksAPI
        [HttpPost]
        public IActionResult PostStock([FromBody] Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            stock.LastUpdated = DateTime.Now;
            var existProductInStock = _context.Stocks.Where(p => p.ProductID == stock.Product.ProductID);
            if(existProductInStock.Count() > 0)
            {
                return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
            }
            _context.Stocks.Add(stock);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StockExists(stock.StockID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetStock", new { id = stock.StockID }, stock);
        }

        // DELETE: api/StocksAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStock(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Stock stock = _context.Stocks.Single(m => m.StockID == id);
            if (stock == null)
            {
                return HttpNotFound();
            }

            _context.Stocks.Remove(stock);
            _context.SaveChanges();

            return Ok(stock);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockExists(int id)
        {
            return _context.Stocks.Count(e => e.StockID == id) > 0;
        }
    }
}
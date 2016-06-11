using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;
using StoreFertilizers.Models.Paging;
using System.Linq.Dynamic;
using System.Collections;

namespace StoreFertilizers.Controllers
{
    [Produces("application/json")]
    [Route("api/ProductsAPI")]
    public class ProductsAPIController : Controller
    {
        private ApplicationDbContext _context;

        public ProductsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetProductsList")]
        public IEnumerable GetProductsList()
        {
            return _context.Products.Include(i => i.ProductType).Include(i => i.UnitType).Select(product => new
            {
                ProductID = product.ProductID,
                ProductNumber = product.ProductNumber,
                ProductTypeName = product.ProductType.Name,
                ProductUnitTypeName = product.UnitType.Name,
                UnitType = product.UnitType,
                Name = product.Name,
                Price = product.Price,
                UnitsPerPackageText = product.UnitsPerPackageText
            });
        }
        // GET: api/ProductsAPI
        [HttpGet]
        public PagedList GetProductsPaging(string searchtext = "", int page = 1, int pageSize = 50, string sortBy = "", string sortDirection = "asc")
        {
            //sortDirection "asc", "desc"
            var pagedRecord = new PagedList();

            var product_result = _context.Products.Include(i => i.ProductType).Include(i => i.UnitType)
                .Select(product => new
                {
                    ProductID = product.ProductID,
                    ProductNumber = product.ProductNumber,
                    ProductTypeName = product.ProductType.Name,
                    ProductName = product.Name,
                    Price = product.Price,
                    UnitsPerPackageText = product.UnitsPerPackageText
                });
            if (!string.IsNullOrEmpty(searchtext))
            {
                product_result = product_result.Where(x =>
                        (!string.IsNullOrEmpty(x.ProductNumber) && x.ProductNumber.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.ProductName) && x.ProductName.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.ProductTypeName) && x.ProductTypeName.Contains(searchtext))
                    );
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                product_result = product_result.OrderBy(sortBy + " " + sortDirection);
            }

            pagedRecord.TotalRecords = product_result.Count();
            pagedRecord.Content = product_result.Skip((page - 1) * pageSize).Take(pageSize);
            pagedRecord.CurrentPage = page;
            pagedRecord.PageSize = pageSize;

            return pagedRecord;
        }


        // GET: api/ProductsAPI/5
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Product product = _context.Products.Include(i => i.ProductType).Include(i => i.UnitType).Single(m => m.ProductID == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return Ok(product);
        }

        // PUT: api/ProductsAPI/5
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != product.ProductID)
            {
                return HttpBadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/ProductsAPI
        [HttpPost]
        public IActionResult PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.Products.Add(product);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetProduct", new { id = product.ProductID }, product);
        }

        // DELETE: api/ProductsAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Product product = _context.Products.Single(m => m.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Count(e => e.ProductID == id) > 0;
        }
    }
}
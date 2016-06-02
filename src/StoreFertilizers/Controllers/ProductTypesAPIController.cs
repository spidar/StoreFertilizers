using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    [Produces("application/json")]
    [Route("api/ProductTypesAPI")]
    public class ProductTypesAPIController : Controller
    {
        private ApplicationDbContext _context;

        public ProductTypesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductTypesAPI
        [HttpGet]
        public IEnumerable<ProductType> GetProductTypes()
        {
            return _context.ProductTypes;
        }

        // GET: api/ProductTypesAPI/5
        [HttpGet("{id}", Name = "GetProductType")]
        public IActionResult GetProductType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            ProductType productType = _context.ProductTypes.Single(m => m.ProductTypeID == id);

            if (productType == null)
            {
                return HttpNotFound();
            }

            return Ok(productType);
        }

        // PUT: api/ProductTypesAPI/5
        [HttpPut("{id}")]
        public IActionResult PutProductType(int id, [FromBody] ProductType productType)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != productType.ProductTypeID)
            {
                return HttpBadRequest();
            }

            _context.Entry(productType).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(id))
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

        // POST: api/ProductTypesAPI
        [HttpPost]
        public IActionResult PostProductType([FromBody] ProductType productType)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.ProductTypes.Add(productType);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductTypeExists(productType.ProductTypeID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetProductType", new { id = productType.ProductTypeID }, productType);
        }

        // DELETE: api/ProductTypesAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProductType(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            ProductType productType = _context.ProductTypes.Single(m => m.ProductTypeID == id);
            if (productType == null)
            {
                return HttpNotFound();
            }

            _context.ProductTypes.Remove(productType);
            _context.SaveChanges();

            return Ok(productType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductTypeExists(int id)
        {
            return _context.ProductTypes.Count(e => e.ProductTypeID == id) > 0;
        }
    }
}
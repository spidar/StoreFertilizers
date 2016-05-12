using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    [Produces("application/json")]
    [Route("api/PurchasesAPI")]
    public class PurchasesAPIController : Controller
    {
        private ApplicationDbContext _context;

        public PurchasesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PurchasesAPI
        [HttpGet]
        public IEnumerable<Purchase> GetPurchases()
        {
            return _context.Purchases.Include(i => i.Product).Include(i => i.UnitType).Include(i => i.Provider).OrderByDescending(i => i.PurchaseID);
        }

        // GET: api/PurchasesAPI/5
        [HttpGet("{id}", Name = "GetPurchase")]
        public IActionResult GetPurchase([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Purchase purchase = _context.Purchases.Single(m => m.PurchaseID == id);

            if (purchase == null)
            {
                return HttpNotFound();
            }

            var product = _context.Products.Where(i => i.ProductID == purchase.ProductID).ToList();
            var unittype = _context.UnitTypes.Where(i => i.UnitTypeID == purchase.UnitTypeID).ToList();
            var provider = _context.Providers.Where(i => i.ProviderID == purchase.ProviderID).ToList();

            return Ok(purchase);
        }

        // PUT: api/PurchasesAPI/5
        [HttpPut("{id}")]
        public IActionResult PutPurchase(int id, [FromBody] Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != purchase.PurchaseID)
            {
                return HttpBadRequest();
            }

            _context.Entry(purchase).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: api/PurchasesAPI
        [HttpPost]
        public IActionResult PostPurchase([FromBody] Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            purchase.Amount = purchase.Qty * purchase.PurchasePricePerUnit;

            _context.Purchases.Add(purchase);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PurchaseExists(purchase.PurchaseID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetPurchase", new { id = purchase.PurchaseID }, purchase);
        }

        // DELETE: api/PurchasesAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeletePurchase(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Purchase purchase = _context.Purchases.Single(m => m.PurchaseID == id);
            if (purchase == null)
            {
                return HttpNotFound();
            }

            _context.Purchases.Remove(purchase);
            _context.SaveChanges();

            return Ok(purchase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchases.Count(e => e.PurchaseID == id) > 0;
        }
    }
}
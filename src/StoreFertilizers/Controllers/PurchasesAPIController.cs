using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;
using System;
using StoreFertilizers.Models.Paging;
//using System.Collections;

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
        [HttpGet("AllPurchases")]
        public IEnumerable<Purchase> GetPurchases()
        {
            return _context.Purchases.Include(i => i.Product).Include(i => i.Provider).OrderByDescending(i => i.PurchaseID);
        }

        [HttpGet]
        public PagedList GetPurchasesPaging(string searchtext, int page = 1, int pageSize = 10, string sortBy = "purchaseID", string sortDirection = "asc")
        {
            var pagedRecord = new PagedList();
            //Order
            if(!string.IsNullOrEmpty(sortDirection) && sortDirection.Equals("desc"))
            {
                switch (sortBy)
                {
                    case "purchaseDate":
                        pagedRecord.Content = _context.Purchases.OrderByDescending(x => x.PurchaseDate).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    case "billNumber":
                        pagedRecord.Content = _context.Purchases.OrderByDescending(x => x.BillNumber).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    case "product":
                        pagedRecord.Content = _context.Purchases.OrderByDescending(x => x.Product.Name).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    case "qty":
                        pagedRecord.Content = _context.Purchases.OrderByDescending(x => x.Qty).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    case "purchasePricePerUnit":
                        pagedRecord.Content = _context.Purchases.OrderByDescending(x => x.PurchasePricePerUnit).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    case "isTax":
                        pagedRecord.Content = _context.Purchases.OrderByDescending(x => x.IsTax).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    case "provider":
                        pagedRecord.Content = _context.Purchases.OrderByDescending(x => x.Provider.Name).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    default:
                        pagedRecord.Content = _context.Purchases.OrderByDescending(x => x.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                }

            }
            else
            {
                switch (sortBy)
                {
                    case "purchaseDate":
                        pagedRecord.Content = _context.Purchases.OrderBy(x => x.PurchaseDate).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    case "billNumber":
                        pagedRecord.Content = _context.Purchases.OrderBy(x => x.BillNumber).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    case "product":
                        pagedRecord.Content = _context.Purchases.OrderBy(x => x.Product.Name).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    case "qty":
                        pagedRecord.Content = _context.Purchases.OrderBy(x => x.Qty).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    case "purchasePricePerUnit":
                        pagedRecord.Content = _context.Purchases.OrderBy(x => x.PurchasePricePerUnit).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    case "isTax":
                        pagedRecord.Content = _context.Purchases.OrderBy(x => x.IsTax).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    case "provider":
                        pagedRecord.Content = _context.Purchases.OrderBy(x => x.Provider.Name).ThenBy(y => y.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                    default:
                        pagedRecord.Content = _context.Purchases.OrderByDescending(x => x.PurchaseID).Skip((page - 1) * pageSize).Take(pageSize).Include(i => i.Product).Include(i => i.Provider);
                        break;
                }
            }

            /*
            pagedRecord.Content = db.Customers
                .Where(x => searchtext == null ||
                        ((x.CustomerID.Contains(searchtext)) ||
                        (x.ContactName.Contains(searchtext)) ||
                        (x.ContactTitle.Contains(searchtext)) ||
                        (x.City.Contains(searchtext)) ||
                        (x.Country.Contains(searchtext))
                    ))
                .OrderBy(sortBy + " " + sortDirection)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Count
            pagedRecord.TotalRecords = db.Customers
                        .Where(x => searchtext == null ||
                                ((x.CustomerID.Contains(searchtext)) ||
                                (x.ContactName.Contains(searchtext)) ||
                                (x.ContactTitle.Contains(searchtext)) ||
                                (x.City.Contains(searchtext)) ||
                                (x.Country.Contains(searchtext))
                            )).Count();
            */
            pagedRecord.CurrentPage = page;
            pagedRecord.PageSize = pageSize;

            return pagedRecord;
        }

        // GET: api/PurchasesAPI/5
        [HttpGet("GetPurchaseTax")]
        public IActionResult GetPurchaseTax([FromRoute] int id)
        {
            var purchases = _context.Purchases.Where(m => m.QtyRemain > 0).Where(n => n.IsTax == true).Include(i => i.Product);

            return Ok(purchases);
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
            //var unittype = _context.UnitTypes.Where(i => i.UnitTypeID == purchase.UnitTypeID).ToList();
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

            purchase.Amount = purchase.Qty * purchase.PurchasePricePerUnit;
            purchase.QtyRemain = purchase.Qty;

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
            purchase.QtyRemain = purchase.Qty;

            _context.Purchases.Add(purchase);

            var productInStock = _context.Stock.Single(i => i.ProductID == purchase.ProductID);
            if(productInStock != null)
            {
                productInStock.Balance += purchase.Qty;
                _context.Entry(productInStock).State = EntityState.Modified;
            }
            else
            {
                Stock newProductInStaok = new Stock()
                {
                    Product = purchase.Product,
                    Balance = purchase.Qty,
                    LastUpdated = DateTime.Now
                };
                _context.Stock.Add(newProductInStaok);
            }

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
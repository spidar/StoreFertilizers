using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;
using StoreFertilizers.Models.Paging;
using System.Linq.Dynamic;

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
        public PagedList GetPurchasesPaging(string searchtext = "", string fromPurchaseDate = "", string toPurchaseDate = "", int page = 1, int pageSize = 50, string sortBy = "", string sortDirection = "desc")
        {
            //sortDirection "asc", "desc"
            var pagedRecord = new PagedList();

            var purchases_result = _context.Purchases.Include(products => products.Product).Include(providers => providers.Provider)
                .Select(purc => new
                {
                    PurchaseID = purc.PurchaseID,
                    PurchaseDate = purc.PurchaseDate,
                    BillNumber = purc.BillNumber,
                    ProductID = purc.ProductID,
                    OrgProductID = purc.ProductID,
                    Product = purc.Product,
                    ProductName = purc.Product.Name,
                    ProductUnitTypeName = purc.Product.UnitType.Name,
                    Qty = purc.Qty,
                    OrgQty = purc.Qty,
                    QtyRemain = purc.QtyRemain,
                    OrgQtyRemain = purc.QtyRemain,
                    PurchasePricePerUnit = purc.PurchasePricePerUnit,
                    //SalePrice = purc.SalePrice,
                    Amount = purc.Amount,
                    IsTax = purc.IsTax,
                    //VAT = purc.VAT,
                    ProviderID = purc.ProviderID,
                    Provider = purc.Provider,
                    ProviderName = purc.Provider.Name,
                    Notes = purc.Notes
                });
            #region Handle from and to purchase date
            DateTime from, to;
            if (!string.IsNullOrEmpty(fromPurchaseDate) && string.IsNullOrEmpty(toPurchaseDate))
            {
                toPurchaseDate = DateTime.Now.ToString();
            }
            if (!string.IsNullOrEmpty(toPurchaseDate) && string.IsNullOrEmpty(fromPurchaseDate))
            {
                fromPurchaseDate = DateTime.MinValue.ToString();
            }
            if (DateTime.TryParse(fromPurchaseDate, out from) && DateTime.TryParse(toPurchaseDate, out to))
            {
                purchases_result = purchases_result.Where(x => x.PurchaseDate.Value.Date >= from.Date && x.PurchaseDate.Value.Date <= to.Date);
            }
            #endregion
            if (!string.IsNullOrEmpty(searchtext))
            {
                purchases_result = purchases_result.Where(x =>
                        (!string.IsNullOrEmpty(x.BillNumber) && x.BillNumber.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.ProductName) && x.ProductName.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.ProviderName) && x.ProviderName.Contains(searchtext))
                    );
            }

            if(!string.IsNullOrEmpty(sortBy))
            {
                purchases_result = purchases_result.OrderBy(sortBy + " " + sortDirection);
            }

            pagedRecord.TotalRecords = purchases_result.Count();
            pagedRecord.Content = purchases_result.Skip((page - 1) * pageSize).Take(pageSize);
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
                        
            Stock orgProductInStock = _context.Stocks.SingleOrDefault(i => i.ProductID == purchase.OrgProductID);
            decimal diffQty = purchase.Qty - purchase.OrgQty;
            if (purchase.Product.ProductID != purchase.OrgProductID)
            {
                purchase.QtyRemain = purchase.Qty;

                #region Handle stock
                //Old one            
                if (orgProductInStock != null)
                {
                    /*
                    if(purchase.Qty >= purchase.OrgQty)
                    {
                        orgProductInStock.Balance -= purchase.OrgQty;
                    }
                    else
                    {
                        orgProductInStock.Balance -= (purchase.OrgQty - purchase.Qty);
                    }
                    */
                    orgProductInStock.Balance -= purchase.OrgQty;
                    orgProductInStock.LastUpdated = DateTime.Now;
                    _context.Entry(orgProductInStock).State = EntityState.Modified;
                }
                //New one
                Stock newProductInStock = _context.Stocks.SingleOrDefault(i => i.ProductID == purchase.Product.ProductID);
                if (newProductInStock != null)
                {
                    newProductInStock.Balance += purchase.Qty;
                    newProductInStock.LastUpdated = DateTime.Now;
                    _context.Entry(newProductInStock).State = EntityState.Modified;
                }
                else
                {
                    Stock pInStock = new Stock()
                    {
                        ProductID = purchase.ProductID,
                        Product = purchase.Product,
                        Balance = purchase.Qty,
                        LastUpdated = DateTime.Now
                    };
                    _context.Stocks.Add(pInStock);
                }
                #endregion
            }else
            {
                purchase.QtyRemain += diffQty;

                #region Handle stock            
                if (orgProductInStock != null && diffQty != 0)
                {
                    orgProductInStock.Balance += diffQty;
                    orgProductInStock.LastUpdated = DateTime.Now;
                    _context.Entry(orgProductInStock).State = EntityState.Modified;
                }
                #endregion
            }

            purchase.Amount = purchase.Qty * purchase.PurchasePricePerUnit;

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

            var productInStock = _context.Stocks.Single(i => i.ProductID == purchase.ProductID);
            if(productInStock != null)
            {
                productInStock.Balance += purchase.Qty;
                productInStock.LastUpdated = DateTime.Now;
                _context.Entry(productInStock).State = EntityState.Modified;
            }
            else
            {
                Stock newProductInStock = new Stock()
                {
                    ProductID = purchase.ProductID,
                    Product = purchase.Product,
                    Balance = purchase.Qty,
                    LastUpdated = DateTime.Now
                };
                _context.Stocks.Add(newProductInStock);
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
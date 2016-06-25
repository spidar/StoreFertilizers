using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;
using StoreFertilizers.Models.Paging;
using System.Linq.Dynamic;
using StoreFertilizers.Models.ModelView;
using Microsoft.AspNet.Cors;

namespace StoreFertilizers.Controllers
{
    [EnableCors("mypolicy")]
    [Produces("application/json")]
    [Route("api/InvoiceDetailsAPI")]
    public class InvoiceDetailsAPIController : Controller
    {
        private ApplicationDbContext _context;

        public InvoiceDetailsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceDetailsAPI
        [HttpGet]
        public PagedList GetInvoiceDetails(string searchtext = "", string isTax = "notax", string fromCreatedDate = "", string toCreatedDate = "", bool groupping = false, int page = 1, int pageSize = 50, string sortBy = "", string sortDirection = "asc")
        {
            //sortDirection "asc", "desc"
            var pagedRecord = new PagedList();

            IEnumerable<InvoiceDetailsView> invoices_details_result = null;
            bool tax = isTax.Equals("tax") ? true : false;
            #region Handle from and to created date
            DateTime from, to;
            if (!string.IsNullOrEmpty(fromCreatedDate) && string.IsNullOrEmpty(toCreatedDate))
            {
                toCreatedDate = DateTime.Now.ToString();
            }
            if (!string.IsNullOrEmpty(toCreatedDate) && string.IsNullOrEmpty(fromCreatedDate))
            {
                fromCreatedDate = DateTime.MinValue.ToString();
            }
            if (DateTime.TryParse(fromCreatedDate, out from) && DateTime.TryParse(toCreatedDate, out to))
            {
                invoices_details_result = _context.InvoiceDetails.Where(x => x.IsTax == tax && x.CreatedDate.Value.Date >= from.Date && x.CreatedDate.Value.Date <= to.Date).Include(p => p.Product.ProductType).Include(p => p.Product.UnitType).
                    Select(details => new InvoiceDetailsView()
                    {
                        InvoiceID = details.InvoiceID,
                        CreatedDate = details.CreatedDate,
                        ProductID = details.ProductID,
                        ProductName = details.Product.Name,
                        ProductTypeName = details.Product.ProductType.Name,
                        ProductUnitTypeName = details.Product.UnitType.Name,
                        Qty = details.Qty,
                        PricePerUnit = details.PricePerUnit,
                        Discount = details.Discount,
                        Amount = details.Amount
                    });
            }
            #endregion
            if (invoices_details_result == null)
            {
                invoices_details_result = _context.InvoiceDetails.Where(x=>x.IsTax == tax).Include(p => p.Product.ProductType).Include(p => p.Product.UnitType).
                    Select(details => new InvoiceDetailsView()
                    {
                        InvoiceID = details.InvoiceID,
                        IsTax = details.IsTax,
                        CreatedDate = details.CreatedDate,
                        ProductID = details.ProductID,
                        ProductName = details.Product.Name,
                        ProductTypeName = details.Product.ProductType.Name,
                        ProductUnitTypeName = details.Product.UnitType.Name,
                        Qty = details.Qty,
                        PricePerUnit = details.PricePerUnit,
                        Discount = details.Discount,
                        Amount = details.Amount
                    }).ToList();
            }
            if (!string.IsNullOrEmpty(searchtext))
            {
                invoices_details_result = invoices_details_result.Where(x =>
                        (!string.IsNullOrEmpty(x.ProductName) && x.ProductName.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.ProductTypeName) && x.ProductTypeName.Contains(searchtext))
                    );
            }
            if (!string.IsNullOrEmpty(sortBy))
            {
                invoices_details_result = invoices_details_result.OrderBy(sortBy + " " + sortDirection);
            }

            pagedRecord.TotalNetAmount = invoices_details_result.Sum(x => x.Amount);
            //pagedRecord.TotalProducts = invoices_details_result.GroupBy(x => x.ProductID).Count();
            pagedRecord.TotalRecords = invoices_details_result.Count();
            invoices_details_result = invoices_details_result.Skip((page - 1) * pageSize).Take(pageSize);
            if (groupping)
            {
                pagedRecord.Content = invoices_details_result.GroupBy(x => x.ProductName)
                    .Select(row => new
                    {
                        ProductID = row.First().ProductID,
                        ProductTypeName = row.First().ProductTypeName,
                        ProductName = row.First().ProductName,
                        ProductUnitTypeName = row.First().ProductUnitTypeName,
                        SumQty = row.Sum(q => q.Qty),
                        SumAmount = row.Sum(a => a.Amount),
                        FullDetails = row.ToList()
                    }).ToList().OrderBy("ProductName " + sortDirection);
            }
            else
            {
                pagedRecord.Content = invoices_details_result;
            }
            //pagedRecord.Content = invoices_details_result.Skip((page - 1) * pageSize).Take(pageSize);
            
            pagedRecord.CurrentPage = page;
            pagedRecord.PageSize = pageSize;

            return pagedRecord;
        }

        [Route("GetInvoiceDetailsByInvoiceID")] /* this route becomes api/[controller]/GetByAdminId */
        public IActionResult GetInvoiceDetailsByInvoiceID([FromQuery] int id)
        {
            var invoicedetails = _context.InvoiceDetails.Where(i => i.InvoiceID == id).ToArray();
            return Ok(invoicedetails);
        }

        // GET: api/InvoiceDetailsAPI/5
        [HttpGet("{id}", Name = "GetInvoiceDetails")]
        public IActionResult GetInvoiceDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            InvoiceDetails invoiceDetails = _context.InvoiceDetails.Single(m => m.InvoiceDetailsID == id);

            if (invoiceDetails == null)
            {
                return HttpNotFound();
            }

            return Ok(invoiceDetails);
        }

        // PUT: api/InvoiceDetailsAPI/5
        [HttpPut("{id}")]
        public IActionResult PutInvoiceDetails(int id, [FromBody] InvoiceDetails invoiceDetails)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != invoiceDetails.InvoiceDetailsID)
            {
                return HttpBadRequest();
            }

            _context.Entry(invoiceDetails).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceDetailsExists(id))
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

        // POST: api/InvoiceDetailsAPI
        [HttpPost]
        public IActionResult PostInvoiceDetails([FromBody] InvoiceDetails invoiceDetails)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.InvoiceDetails.Add(invoiceDetails);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InvoiceDetailsExists(invoiceDetails.InvoiceDetailsID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetInvoiceDetails", new { id = invoiceDetails.InvoiceDetailsID }, invoiceDetails);
        }

        // DELETE: api/InvoiceDetailsAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteInvoiceDetails(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            InvoiceDetails invoiceDetails = _context.InvoiceDetails.Single(m => m.InvoiceDetailsID == id);
            if (invoiceDetails == null)
            {
                return HttpNotFound();
            }

            _context.InvoiceDetails.Remove(invoiceDetails);
            _context.SaveChanges();

            return Ok(invoiceDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceDetailsExists(int id)
        {
            return _context.InvoiceDetails.Count(e => e.InvoiceDetailsID == id) > 0;
        }
    }
}
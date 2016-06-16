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

namespace StoreFertilizers.Controllers
{
    [Produces("application/json")]
    [Route("api/InvoicesAPI")]
    public class InvoicesAPIController : Controller
    {
        private ApplicationDbContext _context;

        public InvoicesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoicesAPI
        [HttpGet("AllInvoices")]
        public IEnumerable<Invoice> GetInvoices()
        {
            return _context.Invoices;
        }

        [HttpGet("CreateNewInvoice")]
        public Invoice CreateNewInvoice(bool isTax)
        {
            Invoice invoice = new Invoice();
            var lastInvoice = _context.Invoices.Where(i => i.IsTax == isTax).OrderByDescending(x => x.InvoiceID).FirstOrDefault();
            string yearString = (DateTime.Now.Year % 100).ToString("00");
            string monthString = DateTime.Now.Month.ToString("00");
            string prefixInvoice = "CT" + (DateTime.Now.Year % 100).ToString("00") + DateTime.Now.Month.ToString("00");
            if (lastInvoice != null)
            {
                if (lastInvoice.InvoiceNumber.Contains("-"))
                {
                    int lastInvoiceNumber;                    
                    string[] invNumberSplit = lastInvoice.InvoiceNumber.Split('-');
                    if(invNumberSplit.Length >= 2)
                    {
                        if (invNumberSplit[0].Equals(prefixInvoice))
                        {
                            //Same month
                            if (int.TryParse(invNumberSplit[1], out lastInvoiceNumber))
                            {
                                invoice.InvoiceNumber = prefixInvoice + "-" + (lastInvoiceNumber + 1).ToString("D5");
                            }
                            else
                            {
                                //Something wrong
                            }
                        }
                        else
                        {
                            //Diff month
                            invoice.InvoiceNumber = prefixInvoice + "-00001";
                        }
                    }                   
                }
            }else
            {
                //First Invoice
                invoice.InvoiceNumber = prefixInvoice + "-00001";
            }
            if (!isTax)
            {
                invoice.InvoiceDetails.Add(new InvoiceDetails());
            }
            invoice.CreatedDate = DateTime.Now;
            return invoice;
        }

        [HttpGet]
        public PagedList GetInvoicesPaging(string searchtext = "", string isTax = "", string fromCreatedDate = "", string toCreatedDate = "", string dueIn = "", int page = 1, int pageSize = 50, string sortBy = "", string sortDirection = "asc")
        {
            //sortDirection "asc", "desc"
            var pagedRecord = new PagedList();
            IEnumerable<InvoiceView> invoices_result = null;
            #region Handle from and to purchase date
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
                invoices_result = _context.Invoices.Where(x => x.CreatedDate.Value.Date >= from.Date && x.CreatedDate.Value.Date <= to.Date).Include(cus => cus.Customer).ToList()
                .Select(invoice => new InvoiceView()
                {
                    InvoiceID = invoice.InvoiceID,
                    InvoiceNumber = invoice.InvoiceNumber,
                    CreatedDate = invoice.CreatedDate,
                    DueDate = invoice.DueDate,
                    CustomerName = (invoice.Customer != null && invoice.Customer.Name != "อื่นๆ") ? invoice.Customer.Name : invoice.CustomerName,
                    Paid = invoice.Paid,
                    NetTotal = invoice.NetTotal,
                    IsTax = invoice.IsTax,
                    Notes = invoice.Notes
                });
            }
            else
            {
                invoices_result = _context.Invoices.Include(cus => cus.Customer).ToList()
                .Select(invoice => new InvoiceView()
                {
                    InvoiceID = invoice.InvoiceID,
                    InvoiceNumber = invoice.InvoiceNumber,
                    CreatedDate = invoice.CreatedDate,
                    DueDate = invoice.DueDate,
                    CustomerName = (invoice.Customer != null && invoice.Customer.Name != "อื่นๆ") ? invoice.Customer.Name : invoice.CustomerName,
                    Paid = invoice.Paid,
                    NetTotal = invoice.NetTotal,
                    IsTax = invoice.IsTax,
                    Notes = invoice.Notes
                });
            }
            #endregion
            #region "Handle DueDate"
            if (!string.IsNullOrEmpty(dueIn))
            {
                invoices_result = invoices_result.Where(x => x.Paid != null && x.Paid.Value == false && x.DueDate != null);
                switch (dueIn)
                {
                    case "over":
                        invoices_result = invoices_result.Where(x => x.DueDate.Value.Date < DateTime.Now.Date);
                        break;
                    case "today":
                        invoices_result = invoices_result.Where(x => x.DueDate.Value.Date == DateTime.Now.Date);
                        break;
                    case "tomorrow":
                        DateTime tomorrow = DateTime.Now.AddDays(1);
                        invoices_result = invoices_result.Where(x => x.DueDate.Value.Date == tomorrow.Date);
                        break;
                    case "next3":
                        DateTime next3 = DateTime.Now.AddDays(3);
                        invoices_result = invoices_result.Where(x => x.DueDate.Value.Date >= DateTime.Now.Date && x.DueDate.Value.Date <= next3.Date);
                        break;
                    case "next7":
                        DateTime next7 = DateTime.Now.AddDays(7);
                        invoices_result = invoices_result.Where(x => x.DueDate.Value.Date >= DateTime.Now.Date && x.DueDate.Value.Date <= next7.Date);
                        break;
                }

            }
            #endregion
            if (!string.IsNullOrEmpty(isTax))
            {
                bool tax = isTax.Equals("tax") ? true : false;
                invoices_result = invoices_result.Where(x => x.IsTax == tax);
            }
            if (!string.IsNullOrEmpty(searchtext))
            {
                invoices_result = invoices_result.Where(x =>
                        (!string.IsNullOrEmpty(x.InvoiceNumber) && x.InvoiceNumber.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.CustomerName) && x.CustomerName.Contains(searchtext)) ||
                        (!string.IsNullOrEmpty(x.Notes) && x.Notes.Contains(searchtext))
                    );
            }
            
            if (!string.IsNullOrEmpty(sortBy))
            {
                invoices_result = invoices_result.OrderBy(sortBy + " " + sortDirection);
            }
            pagedRecord.SumNetTotalEachDay = invoices_result.GroupBy(g => g.CreatedDate).Select(row => new
            {
                CreatedDate = row.First().CreatedDate,
                SumNetTotalPerDay = row.Sum(a => a.NetTotal)
            }).ToList();
            pagedRecord.TotalNetAmount = invoices_result.Sum(x => x.NetTotal);
            pagedRecord.TotalRecords = invoices_result.Count();
            pagedRecord.Content = invoices_result.Skip((page - 1) * pageSize).Take(pageSize);
            pagedRecord.CurrentPage = page;
            pagedRecord.PageSize = pageSize;
            
            return pagedRecord;
        }

        // GET: api/InvoicesAPI/5
        [HttpGet("{id}", Name = "GetInvoice")]
        public IActionResult GetInvoice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Invoice invoice = _context.Invoices.Single(m => m.InvoiceID == id);

            if (invoice == null)
            {
                return HttpNotFound();
            }

            #region "Try to load all child property
            var invoiceDetails = _context.InvoiceDetails.Where(i => i.InvoiceID == invoice.InvoiceID).ToList();

            foreach (var item in invoiceDetails)
            {
                var invoiceDetailsProduct = _context.Products.Where(i => i.ProductID == item.ProductID).ToList();
                //var unitType = _context.UnitTypes.Where(i => i.UnitTypeID == item.UnitTypeID).ToList();
                var productType = _context.ProductTypes.Where(i => i.ProductTypeID == item.Product.ProductTypeID).ToList();
                var unitType2 = _context.UnitTypes.Where(i => i.UnitTypeID == item.Product.UnitTypeID).ToList();
                if (invoice.IsTax)
                {
                    var purchase = _context.Purchases.Where(i => i.PurchaseID == item.PurchaseID).ToList();
                }
                item.OrgProductID = item.ProductID;
                item.OrgQty = item.Qty;
            }

            //if (invoice.EmployeeID != null)
            //{
            //    invoice.Employee = _context.Employees.SingleOrDefault(i => i.EmployeeID == invoice.EmployeeID);
            //}
            if (invoice.CustomerID != null)
            {
                invoice.Customer = _context.Customers.SingleOrDefault(i => i.CustomerID == invoice.CustomerID);
            }
            /*
            if (invoice.PaymentTypeID != null)
            {
                invoice.PaymentType = _context.PaymentTypes.SingleOrDefault(i => i.PaymentTypeID == invoice.PaymentTypeID);
            }
            if (invoice.BankID != null)
            {
                invoice.Bank = _context.Banks.SingleOrDefault(i => i.BankID == invoice.BankID);
            }
            */
            #endregion

            return Ok(invoice);
        }

        // PUT: api/InvoicesAPI/5
        [HttpPut("{id}")]
        public IActionResult PutInvoice(int id, [FromBody] Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != invoice.InvoiceID)
            {
                return HttpBadRequest();
            }

            //var modifiedEntries = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).Select(x => x.Entity).ToList();
            //var addEntries = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).Select(x => x.Entity).ToList();
            //var deletedEntries = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted).Select(x => x.Entity).ToList();
            if (invoice.Customer != null)
            {
                //invoice.CustomerName = invoice.Customer.Name;
            }
            _context.Entry(invoice).State = EntityState.Modified;

            #region "Handle Add detailInvoices"
            var addNews = invoice.InvoiceDetails.Where(i => i.InvoiceDetailsID == 0).ToList();
            Stock productInStock = null;
            Purchase purcahseItem = null;

            foreach (var item in addNews)
            {
                if (invoice.IsTax == false)
                {
                    #region "Handle Stock"
                    productInStock = _context.Stocks.SingleOrDefault(i => i.ProductID == item.Product.ProductID);
                    if (productInStock == null)
                    {
                        return HttpBadRequest("ไม่พบสินค้า " + item.Product.Name + " ในคลัง");
                    }
                    //Create case should be strang forward.
                    productInStock.Balance -= item.Qty;
                    _context.Entry(productInStock).State = EntityState.Modified;
                    #endregion
                }
                else
                {
                    //Handle QtyRemain
                    purcahseItem = _context.Purchases.SingleOrDefault(i => i.PurchaseID == item.PurchaseID);
                    purcahseItem.QtyRemain = item.Purchase.QtyRemain;// - item.Qty;
                    _context.Entry(purcahseItem).State = EntityState.Modified;
                }
                invoice.InvoiceDetails.Remove(item);
                item.CreatedDate = invoice.CreatedDate;
                _context.InvoiceDetails.Add(item);
            }
            #endregion

            #region "Handle Edit and Delete"
            Stock orgProductInStock = null;
            foreach (var item in invoice.InvoiceDetails)
            {
                //item.CreatedDate = invoice.CreatedDate;
                if (item.InvoiceDetailsID > 0)
                {
                    bool isDeleted = (item.IsDeleted != null && item.IsDeleted.Value);
                    if (invoice.IsTax == false)
                    {
                        orgProductInStock = _context.Stocks.SingleOrDefault(i => i.ProductID == item.OrgProductID);
                        if (item.Product.ProductID != item.OrgProductID)
                        {
                            productInStock = _context.Stocks.SingleOrDefault(i => i.ProductID == item.Product.ProductID);
                        }
                    }
                    else
                    {
                        purcahseItem = _context.Purchases.SingleOrDefault(i => i.PurchaseID == item.PurchaseID);
                    }
                    if (isDeleted)
                    {
                        _context.InvoiceDetails.Remove(item);
                        if (invoice.IsTax == false)
                        {
                            #region "Handle Stock"
                            orgProductInStock.Balance += item.OrgQty;
                            _context.Entry(orgProductInStock).State = EntityState.Modified;
                            #endregion
                        }
                        else
                        {
                            //Handle QtyRemain
                            purcahseItem.QtyRemain = item.Purchase.QtyRemain;// item.QtyRemain - item.Qty;
                            _context.Entry(purcahseItem).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        if (invoice.IsTax == false)
                        {
                            #region "Handle Stock"
                            if (item.Product.ProductID == item.OrgProductID)
                            {
                                if (item.Qty != item.OrgQty)
                                {
                                    orgProductInStock.Balance += item.OrgQty;
                                    orgProductInStock.Balance -= item.Qty;
                                    _context.Entry(orgProductInStock).State = EntityState.Modified;
                                }
                            }
                            else
                            {
                                orgProductInStock.Balance += item.OrgQty;
                                productInStock.Balance -= item.Qty;
                                _context.Entry(productInStock).State = EntityState.Modified;
                                _context.Entry(orgProductInStock).State = EntityState.Modified;
                            }
                            #endregion
                        }
                        else
                        {
                            //Handle QtyRemain
                            purcahseItem.QtyRemain = item.Purchase.QtyRemain;// item.QtyRemain - item.Qty;
                            _context.Entry(purcahseItem).State = EntityState.Modified;
                        }
                        _context.Entry(item).State = EntityState.Modified;
                    }
                }
            }
            #endregion
                        
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return HttpNotFound();
                }
                else
                {
                    throw;
                }
            }
            foreach(var item in invoice.InvoiceDetails)
            {
                item.OrgProductID = item.Product.ProductID;
                item.OrgQty = item.Qty;
            }
            //return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
            return CreatedAtRoute("GetInvoice", new { id = invoice.InvoiceID }, invoice);
        }

        // POST: api/InvoicesAPI
        [HttpPost]
        public IActionResult PostInvoice([FromBody] Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            if(invoice.Customer != null)
            {
                invoice.CustomerName = invoice.Customer.Name;
            }
            Stock productInStock = null;
            foreach (var item in invoice.InvoiceDetails)
            {
                item.CreatedDate = invoice.CreatedDate;
                if (invoice.IsTax == false)
                {
                    #region "Handle Stock"
                    productInStock = _context.Stocks.SingleOrDefault(i => i.ProductID == item.Product.ProductID);
                    if (productInStock == null)
                    {
                        return HttpBadRequest("ไม่พบสินค้า " + item.Product.Name + " ในคลัง");
                    }
                    //Create case should be strang forward.
                    productInStock.Balance -= item.Qty;
                    _context.Entry(productInStock).State = EntityState.Modified;
                    #endregion
                }
                else
                {
                    //Handle QtyRemain
                    Purchase purcahseItem = _context.Purchases.SingleOrDefault(i => i.PurchaseID == item.PurchaseID);
                    purcahseItem.QtyRemain = item.Purchase.QtyRemain;// - item.Qty;
                    _context.Entry(purcahseItem).State = EntityState.Modified;
                }

                item.OrgProductID = item.Product.ProductID;
                item.OrgQty = item.Qty;
            }
            _context.Invoices.Add(invoice);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InvoiceExists(invoice.InvoiceID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetInvoice", new { id = invoice.InvoiceID }, invoice);
        }

        // DELETE: api/InvoicesAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Invoice invoice = _context.Invoices.Single(m => m.InvoiceID == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            _context.Invoices.Remove(invoice);
            _context.SaveChanges();

            return Ok(invoice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Count(e => e.InvoiceID == id) > 0;
        }
    }
}
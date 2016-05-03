using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    [Produces("application/json")]
    [Route("api/PaymentTypesAPI")]
    public class PaymentTypesAPIController : Controller
    {
        private ApplicationDbContext _context;

        public PaymentTypesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PaymentTypesAPI
        [HttpGet]
        public IEnumerable<PaymentType> GetPaymentTypes()
        {
            return _context.PaymentTypes;
        }

        // GET: api/PaymentTypesAPI/5
        [HttpGet("{id}", Name = "GetPaymentType")]
        public IActionResult GetPaymentType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            PaymentType paymentType = _context.PaymentTypes.Single(m => m.PaymentTypeID == id);

            if (paymentType == null)
            {
                return HttpNotFound();
            }

            return Ok(paymentType);
        }

        // PUT: api/PaymentTypesAPI/5
        [HttpPut("{id}")]
        public IActionResult PutPaymentType(int id, [FromBody] PaymentType paymentType)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != paymentType.PaymentTypeID)
            {
                return HttpBadRequest();
            }

            _context.Entry(paymentType).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTypeExists(id))
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

        // POST: api/PaymentTypesAPI
        [HttpPost]
        public IActionResult PostPaymentType([FromBody] PaymentType paymentType)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.PaymentTypes.Add(paymentType);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PaymentTypeExists(paymentType.PaymentTypeID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetPaymentType", new { id = paymentType.PaymentTypeID }, paymentType);
        }

        // DELETE: api/PaymentTypesAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeletePaymentType(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            PaymentType paymentType = _context.PaymentTypes.Single(m => m.PaymentTypeID == id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }

            _context.PaymentTypes.Remove(paymentType);
            _context.SaveChanges();

            return Ok(paymentType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentTypeExists(int id)
        {
            return _context.PaymentTypes.Count(e => e.PaymentTypeID == id) > 0;
        }
    }
}
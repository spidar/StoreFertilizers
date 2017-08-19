using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;
using Microsoft.AspNet.Cors;

namespace StoreFertilizers.Controllers
{
    [EnableCors("mypolicy")]
    [Produces("application/json")]
    [Route("api/PromotionsAPI")]
    public class PromotionsAPIController : Controller
    {
        private ApplicationDbContext _context;

        public PromotionsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PromotionsAPI
        [HttpGet]
        public IEnumerable<Promotion> GetPromotions()
        {
            return _context.Promotions;
        }

        // GET: api/PromotionsAPI/5
        [HttpGet("{id}", Name = "GetPromotion")]
        public IActionResult GetPromotion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Promotion Promotion = _context.Promotions.Single(m => m.PromotionID == id);

            if (Promotion == null)
            {
                return HttpNotFound();
            }

            return Ok(Promotion);
        }

        // PUT: api/PromotionsAPI/5
        [HttpPut("{id}")]
        public IActionResult PutPromotion(int id, [FromBody] Promotion Promotion)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != Promotion.PromotionID)
            {
                return HttpBadRequest();
            }

            _context.Entry(Promotion).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromotionExists(id))
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

        // POST: api/PromotionsAPI
        [HttpPost]
        public IActionResult PostPromotion([FromBody] Promotion Promotion)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.Promotions.Add(Promotion);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PromotionExists(Promotion.PromotionID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetPromotion", new { id = Promotion.PromotionID }, Promotion);
        }

        // DELETE: api/PromotionsAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeletePromotion(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Promotion Promotion = _context.Promotions.Single(m => m.PromotionID == id);
            if (Promotion == null)
            {
                return HttpNotFound();
            }

            _context.Promotions.Remove(Promotion);
            _context.SaveChanges();

            return Ok(Promotion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PromotionExists(int id)
        {
            return _context.Promotions.Count(e => e.PromotionID == id) > 0;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    [Produces("application/json")]
    [Route("api/UnitTypesAPI")]
    public class UnitTypesAPIController : Controller
    {
        private ApplicationDbContext _context;

        public UnitTypesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UnitTypesAPI
        [HttpGet]
        public IEnumerable<UnitType> GetUnitTypes()
        {
            return _context.UnitTypes;
        }

        // GET: api/UnitTypesAPI/5
        [HttpGet("{id}", Name = "GetUnitType")]
        public IActionResult GetUnitType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            UnitType unitType = _context.UnitTypes.Single(m => m.UnitTypeID == id);

            if (unitType == null)
            {
                return HttpNotFound();
            }

            return Ok(unitType);
        }

        // PUT: api/UnitTypesAPI/5
        [HttpPut("{id}")]
        public IActionResult PutUnitType(int id, [FromBody] UnitType unitType)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != unitType.UnitTypeID)
            {
                return HttpBadRequest();
            }

            _context.Entry(unitType).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitTypeExists(id))
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

        // POST: api/UnitTypesAPI
        [HttpPost]
        public IActionResult PostUnitType([FromBody] UnitType unitType)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.UnitTypes.Add(unitType);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UnitTypeExists(unitType.UnitTypeID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetUnitType", new { id = unitType.UnitTypeID }, unitType);
        }

        // DELETE: api/UnitTypesAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUnitType(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            UnitType unitType = _context.UnitTypes.Single(m => m.UnitTypeID == id);
            if (unitType == null)
            {
                return HttpNotFound();
            }

            _context.UnitTypes.Remove(unitType);
            _context.SaveChanges();

            return Ok(unitType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UnitTypeExists(int id)
        {
            return _context.UnitTypes.Count(e => e.UnitTypeID == id) > 0;
        }
    }
}
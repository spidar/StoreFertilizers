using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    [Produces("application/json")]
    [Route("api/BanksAPI")]
    public class BanksAPIController : Controller
    {
        private ApplicationDbContext _context;

        public BanksAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BanksAPI
        [HttpGet]
        public IEnumerable<Bank> GetBanks()
        {
            return _context.Banks;
        }

        // GET: api/BanksAPI/5
        [HttpGet("{id}", Name = "GetBank")]
        public IActionResult GetBank([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Bank bank = _context.Banks.Single(m => m.BankID == id);

            if (bank == null)
            {
                return HttpNotFound();
            }

            return Ok(bank);
        }

        // PUT: api/BanksAPI/5
        [HttpPut("{id}")]
        public IActionResult PutBank(int id, [FromBody] Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != bank.BankID)
            {
                return HttpBadRequest();
            }

            _context.Entry(bank).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankExists(id))
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

        // POST: api/BanksAPI
        [HttpPost]
        public IActionResult PostBank([FromBody] Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.Banks.Add(bank);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BankExists(bank.BankID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetBank", new { id = bank.BankID }, bank);
        }

        // DELETE: api/BanksAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBank(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Bank bank = _context.Banks.Single(m => m.BankID == id);
            if (bank == null)
            {
                return HttpNotFound();
            }

            _context.Banks.Remove(bank);
            _context.SaveChanges();

            return Ok(bank);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BankExists(int id)
        {
            return _context.Banks.Count(e => e.BankID == id) > 0;
        }
    }
}
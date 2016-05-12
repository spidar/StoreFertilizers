using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using StoreFertilizers.Models;

namespace StoreFertilizers.Controllers
{
    [Produces("application/json")]
    [Route("api/ProvidersAPI")]
    public class ProvidersAPIController : Controller
    {
        private ApplicationDbContext _context;

        public ProvidersAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProvidersAPI
        [HttpGet]
        public IEnumerable<Provider> GetProviders()
        {
            return _context.Providers;
        }

        // GET: api/ProvidersAPI/5
        [HttpGet("{id}", Name = "GetProvider")]
        public IActionResult GetProvider([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Provider provider = _context.Providers.Single(m => m.ProviderID == id);

            if (provider == null)
            {
                return HttpNotFound();
            }

            return Ok(provider);
        }

        // PUT: api/ProvidersAPI/5
        [HttpPut("{id}")]
        public IActionResult PutProvider(int id, [FromBody] Provider provider)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != provider.ProviderID)
            {
                return HttpBadRequest();
            }

            _context.Entry(provider).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderExists(id))
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

        // POST: api/ProvidersAPI
        [HttpPost]
        public IActionResult PostProvider([FromBody] Provider provider)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.Providers.Add(provider);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProviderExists(provider.ProviderID))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetProvider", new { id = provider.ProviderID }, provider);
        }

        // DELETE: api/ProvidersAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProvider(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Provider provider = _context.Providers.Single(m => m.ProviderID == id);
            if (provider == null)
            {
                return HttpNotFound();
            }

            _context.Providers.Remove(provider);
            _context.SaveChanges();

            return Ok(provider);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProviderExists(int id)
        {
            return _context.Providers.Count(e => e.ProviderID == id) > 0;
        }
    }
}
using DisasterAlleviationPOE.AppData;
using DisasterAlleviationPOE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationPOE.Controllers
{
    public class DisastersController : Controller
    {

        private readonly DisasterAlleviationContext alleviationContext;

        public DisastersController(DisasterAlleviationContext context)
        {
            alleviationContext = context;
        }

        // GET: DisastersController
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var disasters = alleviationContext.Disasters.Include(a => a.RequiredAidType);
            return View(await disasters.ToListAsync());
        }

        // GET: DisastersController/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disaster = await alleviationContext.Disasters
                .Include(a => a.RequiredAidType)
                .FirstOrDefaultAsync(d => d.DisasterID == id);
            if (disaster == null)
            {
                return NotFound();
            }

            return View(disaster);
        }

        // GET: DisastersController/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["RequiredAidTypeID"] = new SelectList(alleviationContext.RequiredAidTypes, "RequiredAidTypeID", "RequiredAidName");
            return View();
        }

        // POST: DisastersController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisasterID,StartDate,EndDate,Description,Location,RequiredAidTypeID")] Disasters disaster)
        {
            if (ModelState.IsValid)
            {
                alleviationContext.Add(disaster);
                await alleviationContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequiredAidTypeID"] = new SelectList(alleviationContext.RequiredAidTypes, "RequiredAidTypeID", "RequiredAidName", disaster.RequiredAidTypeID);
            return View(disaster);
        }

        // GET: DisastersController/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disasters = await alleviationContext.Disasters.FindAsync(id);
            if (disasters == null)
            {
                return NotFound();
            }
            ViewData["RequiredAidTypeID"] = new SelectList(alleviationContext.RequiredAidTypes, "RequiredAidTypeID", "RequiredAidName", disasters.RequiredAidTypeID);
            return View(disasters);
        }
        // POST: DisastersController/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisasterID,StartDate,EndDate,Description,Location,RequiredAidTypeID")] Disasters disasters)
        {
            if (id != disasters.DisasterID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    alleviationContext.Update(disasters);
                    await alleviationContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisasterExists(disasters.DisasterID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequiredAidTypeID"] = new SelectList(alleviationContext.RequiredAidTypes, "RequiredAidTypeID", "RequiredAidName", disasters.RequiredAidTypeID);
            return View(disasters);
        }

        // GET: DisastersController/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disasters = await alleviationContext.Disasters
                .Include(d => d.RequiredAidType)
                .FirstOrDefaultAsync(d => d.DisasterID == id);
            if (disasters == null)
            {
                return NotFound();
            }

            return View(disasters);
        }

        // POST: DisastersController/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disasters = await alleviationContext.Disasters.FindAsync(id);
            alleviationContext.Disasters.Remove(disasters);
            await alleviationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisasterExists(int id)
        {
            return alleviationContext.Disasters.Any(d => d.DisasterID == id);
        }
    }
}

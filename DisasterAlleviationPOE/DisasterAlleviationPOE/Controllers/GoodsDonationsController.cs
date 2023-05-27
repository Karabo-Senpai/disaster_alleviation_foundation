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
    public class GoodsDonationsController : Controller

    {
        // GET: GoodsDonationsController

        private readonly DisasterAlleviationContext alleviationContext;

        public GoodsDonationsController(DisasterAlleviationContext context)
        {
            alleviationContext = context;
        }



        [Authorize]
        public async Task<IActionResult> Index()
        {

            var goodDonations = alleviationContext.GoodsDonations.
                Include(gd => gd.Category)
                .Include(gd => gd.Disaster)
                .AsNoTracking();
            return View(await goodDonations.ToListAsync());
        }


        // GET: GoodsDonationsController/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goodDonations = await alleviationContext.GoodsDonations
                .Include(gd => gd.Category)
                .Include(gd => gd.Disaster)
                .AsNoTracking()
                .FirstOrDefaultAsync(gd => gd.GoodsDonationID == id);
            if (goodDonations == null)
            {
                return NotFound();
            }

            return View(goodDonations);
        }

        // GET: GoodsDonationsController/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(alleviationContext.Categories, "CategoryID", "CategoryName");
            ViewData["DisasterID"] = new SelectList(alleviationContext.Disasters, "DisasterID", "DisasterID");
            return View();
        }

        // POST: GoodsDonationsController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GoodID,DonationDate,NumberOfItems,Description,DonorName,CategoryID,DisasterID")] GoodsDonations goodsDonations)
        {
            if (ModelState.IsValid)
            {
                alleviationContext.Add(goodsDonations);
                await alleviationContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(alleviationContext.Categories, "CategoryID", "CategoryName", goodsDonations.CategoryID);; ;
            ViewData["DisasterID"] = new SelectList(alleviationContext.Disasters, "DisasterID", "DisasterID", goodsDonations.DisasterID);
            return View(goodsDonations);
        }

        // GET: GoodsDonationsController/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goodsDonations = await alleviationContext.GoodsDonations.FindAsync(id);
            if (goodsDonations == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(alleviationContext.Categories, "CategoryID", "CategoryName", goodsDonations.CategoryID);
            ViewData["DisasterID"] = new SelectList(alleviationContext.Disasters, "DisasterID", "DisasterID", goodsDonations.DisasterID);
            return View(goodsDonations);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GoodsDonationID,DonationDate,NumberOfItems,Description,DonorName,CategoryID,DisasterID")] GoodsDonations goodsDonations)
        {
            if (id != goodsDonations.GoodsDonationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    alleviationContext.Update(goodsDonations);
                    await alleviationContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodsDonationsExists(goodsDonations.GoodsDonationID))
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
            ViewData["CategoryID"] = new SelectList(alleviationContext.Categories, "CategoryID", "CategoryID", goodsDonations.CategoryID);
            ViewData["DisasterID"] = new SelectList(alleviationContext.Disasters, "DisasterID", "DisasterID", goodsDonations.DisasterID);
            return View(goodsDonations);
        }

        // GET: Goods/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goodsDonations = await alleviationContext.GoodsDonations
                .Include(gd => gd.Category)
                .Include(gd => gd.Disaster)
                .FirstOrDefaultAsync(gd => gd.GoodsDonationID == id);
            if (goodsDonations == null)
            {
                return NotFound();
            }

            return View(goodsDonations);
        }

        // POST: Goods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var good = await alleviationContext.GoodsDonations.FindAsync(id);
            alleviationContext.GoodsDonations.Remove(good);
            await alleviationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoodsDonationsExists(int id)
        {
            return alleviationContext.GoodsDonations.Any(gd => gd.GoodsDonationID == id);
        }
    }
}


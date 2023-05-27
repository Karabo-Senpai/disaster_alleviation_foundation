using DisasterAlleviationPOE.AppData;
using DisasterAlleviationPOE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationPOE.Controllers
{
    public class MonetaryDonationsController : Controller
    {
        // GET: MonetaryDonationsController


        //Adding Database Context 
        private readonly DisasterAlleviationContext alleviationContext;

        public decimal totalMonetaryDonations { get; set; }


        public MonetaryDonationsController(DisasterAlleviationContext context)
        {
            alleviationContext = context;
          
        }


        //Getting Sum Total Of All Monetary Donations Made
        public decimal getTotalMonetaryDonations()
        {
            totalMonetaryDonations = alleviationContext.MonetaryDonations.Sum(c => c.DonationAmount);
            return totalMonetaryDonations;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            totalMonetaryDonations = alleviationContext.MonetaryDonations.Sum(c => c.DonationAmount);
            ViewBag.totalMonetaryDonations = totalMonetaryDonations.ToString("C0");
            return View(await alleviationContext.MonetaryDonations.ToListAsync());


        }

        // GET: MonetaryDonationsController/Details/5

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monetary = await alleviationContext.MonetaryDonations
                .FirstOrDefaultAsync(h => h.MonetaryID == id);
            if (monetary == null)
            {
                return NotFound();
            }

            return View(monetary);
        }
        // GET: MonetaryDonationsController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: MonetaryDonationsController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MonetaryID,DonationDate,DonationAmount,DonorName")] MonetaryDonations monetaryDonations)
        {

            if (ModelState.IsValid)
            {

                alleviationContext.Add(monetaryDonations);
                await alleviationContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(monetaryDonations);
        }

        // GET: MonetaryDonationsController/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monetary = await alleviationContext.MonetaryDonations.FindAsync(id);
            if (monetary == null)
            {
                return NotFound();
            }
            return View(monetary);
        }

        // POST: MonetaryDonationsController/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MonetaryID,DonationDate,DonationAmount,DonorName")] MonetaryDonations monetary)
        {
            if (id != monetary.MonetaryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    alleviationContext.Update(monetary);
                    await alleviationContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonetaryDonationExists(monetary.MonetaryID))
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
            return View(monetary);
        }

        // GET: MonetaryDonationsController/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monetary = await alleviationContext.MonetaryDonations
                .FirstOrDefaultAsync(o => o.MonetaryID == id);
            if (monetary == null)
            {
                return NotFound();
            }

            return View(monetary);
        }

        // POST: MonetaryDonationsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monetary = await alleviationContext.MonetaryDonations.FindAsync(id);
            alleviationContext.MonetaryDonations.Remove(monetary);
            await alleviationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool MonetaryDonationExists(int id)
        {
            return alleviationContext.MonetaryDonations.Any(w => w.MonetaryID == id);
        }

    }
}

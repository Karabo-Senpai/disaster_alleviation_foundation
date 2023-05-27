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
    public class PurchaseGoodsController : Controller
    {

        private readonly DisasterAlleviationContext alleviationContext;

        public decimal totalMonetaryDonations { get; set; }
        public decimal goodsPurchasePrice { get; set; }
        public decimal amountRemaining { get; set; }

        public PurchaseGoodsController(DisasterAlleviationContext context)
        {
            alleviationContext = context;
        }

        // GET: PurchaseGoodsController
        [Authorize]
        public async Task<IActionResult> Index()
        {
            totalMonetaryDonations = alleviationContext.MonetaryDonations
               .Sum(m => m.DonationAmount);

            goodsPurchasePrice = alleviationContext.PurchaseGoods.Sum(pa => pa.PurchaseAmount);

            ViewBag.totalMonetaryDonations = totalMonetaryDonations.ToString("C0");
            var goodspurchases = alleviationContext.PurchaseGoods.Include(pg => pg.Disasters).Include(pg => pg.MonetaryDonations);
            updateBalance();

            return View(await goodspurchases.ToListAsync());
        }


        public void updateBalance()
        {
            totalMonetaryDonations = alleviationContext.MonetaryDonations
           .Sum(md => md.DonationAmount);

            goodsPurchasePrice = alleviationContext.PurchaseGoods.Sum(pg => pg.PurchaseAmount);

            amountRemaining = totalMonetaryDonations - goodsPurchasePrice;

            ViewBag.totalMonetaryDonations = amountRemaining.ToString("C0");
        }


        public void increaceBalance()
        {
            totalMonetaryDonations = alleviationContext.MonetaryDonations
           .Sum(md => md.DonationAmount);

            goodsPurchasePrice = alleviationContext.PurchaseGoods.Sum(pg => pg.PurchaseAmount);

            amountRemaining = totalMonetaryDonations + goodsPurchasePrice;

            ViewBag.totalMonetaryDonations = amountRemaining.ToString("C0");
        }




        // GET: PurchaseGoodsController/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseGoods = await alleviationContext.PurchaseGoods
                .Include(pg => pg.Disasters)
                .Include(pg => pg.MonetaryDonations)
                .FirstOrDefaultAsync(gp => gp.GoodsPurchaseID == id);
            if (purchaseGoods == null)
            {
                return NotFound();
            }
            updateBalance();
            return View(purchaseGoods);
        }
        // GET: PurchaseGoodsController/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["DisasterID"] = new SelectList(alleviationContext.Disasters, "DisasterID", "Description");
            ViewData["MonetaryID"] = new SelectList(alleviationContext.MonetaryDonations, "MonetaryID", "MonetaryID");
            //Calling Method To Update The Current Balance
            updateBalance();
            return View();
        }

        // POST: PurchaseGoodsController/Create

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GoodsPurchaseID,DisasterID,MonetaryID,Description,PurchaseAmount")] PurchaseGoods purchaseGoods)
        {
            if (ModelState.IsValid)
            {
                alleviationContext.Add(purchaseGoods);
                await alleviationContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisasterID"] = new SelectList(alleviationContext.Disasters, "DisasterID", "Description", purchaseGoods.DisasterID);
            ViewData["MonetaryID"] = new SelectList(alleviationContext.MonetaryDonations, "MonetaryID", "MonetaryID", purchaseGoods.MonetaryID);

            //Calling Method To Update The Current Balance

            updateBalance();
            return View(purchaseGoods);
        }

        // GET: PurchaseGoodsController/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseGoods = await alleviationContext.PurchaseGoods.FindAsync(id);
            if (purchaseGoods == null)
            {
                return NotFound();
            }
            ViewData["DisasterID"] = new SelectList(alleviationContext.Disasters, "DisasterID", "Description", purchaseGoods.DisasterID);
            ViewData["MonetaryID"] = new SelectList(alleviationContext.MonetaryDonations, "MonetaryID", "MonetaryID", purchaseGoods.MonetaryID);
            return View(purchaseGoods);
        }

        // POST: PurchaseGoodsController/Edit/5

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GoodsPurchaseID,DisasterID,MonetaryID,Description,PurchaseAmount")] PurchaseGoods purchaseGoods)
        {
            if (id != purchaseGoods.GoodsPurchaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    alleviationContext.Update(purchaseGoods);
                    await alleviationContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseGoodsExit(purchaseGoods.GoodsPurchaseID))
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
            ViewData["DisasterID"] = new SelectList(alleviationContext.Disasters, "DisasterID", "Description", purchaseGoods.GoodsPurchaseID);
            ViewData["MonetaryID"] = new SelectList(alleviationContext.MonetaryDonations, "MonetaryID", "MonetaryID", purchaseGoods.MonetaryID);
            return View(purchaseGoods);
        }


        // GET: PurchaseGoodsController/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseGoods = await alleviationContext.PurchaseGoods
                .Include(pg => pg.Disasters)
                .Include(pg => pg.MonetaryDonations)
                .FirstOrDefaultAsync(pg => pg.GoodsPurchaseID == id);
            //Method to increase balance 
            increaceBalance();

            if (purchaseGoods == null)
            {
                return NotFound();
            }

            return View(purchaseGoods);
        }

        // POST: PurchaseGoodsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var goodsPurchase = await alleviationContext.PurchaseGoods.FindAsync(id);
            alleviationContext.PurchaseGoods.Remove(goodsPurchase);
            
            //Method to increase balance
            increaceBalance();
            await alleviationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseGoodsExit(int id)
        {
            return alleviationContext.PurchaseGoods.Any(pg => pg.GoodsPurchaseID == id);
        }
    }
}
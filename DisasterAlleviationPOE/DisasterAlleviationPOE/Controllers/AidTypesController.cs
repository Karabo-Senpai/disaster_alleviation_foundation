using DisasterAlleviationPOE.AppData;
using DisasterAlleviationPOE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace DisasterAlleviationPOE.Controllers
{
    public class AidTypesController : Controller
    {

        private readonly DisasterAlleviationContext alleviationContext;

        public AidTypesController(DisasterAlleviationContext context)
        {
            alleviationContext = context;
        }



        // GET: AidTypesController
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await alleviationContext.RequiredAidTypes.ToListAsync());
        }

        // GET: AidTypesController/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requiredAid = await alleviationContext.RequiredAidTypes
                .FirstOrDefaultAsync(ra => ra.RequiredAidTypeID == id);
            if (requiredAid == null)
            {
                return NotFound();
            }

            return View(requiredAid);
        }

        // GET: AidTypesController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AidTypesController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequiredAidTypeID,RequiredAidName")] RequiredAidType requiredAidType)
        {
            if (ModelState.IsValid)
            {
                alleviationContext.Add(requiredAidType);
                await alleviationContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(requiredAidType);
        }

        // GET: AidTypesController/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requiredAid = await alleviationContext.RequiredAidTypes.FindAsync(id);
            if (requiredAid == null)
            {
                return NotFound();
            }
            return View(requiredAid);
        }
        // POST: AidTypesController/Edit/5


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequiredAidTypeID,RequiredAidName")] RequiredAidType requiredAidType)
        {
            if (id != requiredAidType.RequiredAidTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    alleviationContext.Update(requiredAidType);
                    await alleviationContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequiredAidExists(requiredAidType.RequiredAidTypeID))
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
            return View(requiredAidType);
        }


        // GET: AidTypesController/Delete/5

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requiredAidType = await alleviationContext.RequiredAidTypes
                .FirstOrDefaultAsync(ra => ra.RequiredAidTypeID == id);
            if (requiredAidType == null)
            {
                return NotFound();
            }

            return View(requiredAidType);
        }

        // POST: AidTypesController/Delete/5


        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requiredAidType = await alleviationContext.RequiredAidTypes.FindAsync(id);
            alleviationContext.RequiredAidTypes.Remove(requiredAidType);
            await alleviationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //Method To Check iF Aid Exists
        private bool RequiredAidExists(int requiredAidTypeID)
        {
            return alleviationContext.RequiredAidTypes.Any(ra => ra.RequiredAidTypeID ==requiredAidTypeID);
        }

    }
}
